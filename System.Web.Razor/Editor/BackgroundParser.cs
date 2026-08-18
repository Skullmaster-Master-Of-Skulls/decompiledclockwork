using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using System.Web.Razor.Utils;

namespace System.Web.Razor.Editor
{
	// Token: 0x02000007 RID: 7
	internal class BackgroundParser : IDisposable
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000028D4 File Offset: 0x00000AD4
		public BackgroundParser(RazorEngineHost host, string fileName)
		{
			this._main = new BackgroundParser.MainThreadState(fileName);
			this._bg = new BackgroundParser.BackgroundThread(this._main, host, fileName);
			this._main.ResultsReady += delegate(object sender, DocumentParseCompleteEventArgs args)
			{
				this.OnResultsReady(args);
			};
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000041 RID: 65 RVA: 0x00002924 File Offset: 0x00000B24
		// (remove) Token: 0x06000042 RID: 66 RVA: 0x0000295C File Offset: 0x00000B5C
		public event EventHandler<DocumentParseCompleteEventArgs> ResultsReady;

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002991 File Offset: 0x00000B91
		public bool IsIdle
		{
			get
			{
				return this._main.IsIdle;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000299E File Offset: 0x00000B9E
		public void Start()
		{
			this._bg.Start();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029AB File Offset: 0x00000BAB
		public void Cancel()
		{
			this._main.Cancel();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000029B8 File Offset: 0x00000BB8
		public void QueueChange(TextChange change)
		{
			this._main.QueueChange(change);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000029C6 File Offset: 0x00000BC6
		public void Dispose()
		{
			this._main.Cancel();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029D3 File Offset: 0x00000BD3
		public IDisposable SynchronizeMainThreadState()
		{
			return this._main.Lock();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029E0 File Offset: 0x00000BE0
		protected virtual void OnResultsReady(DocumentParseCompleteEventArgs args)
		{
			EventHandler<DocumentParseCompleteEventArgs> resultsReady = this.ResultsReady;
			if (resultsReady != null)
			{
				resultsReady(this, args);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029FF File Offset: 0x00000BFF
		internal static bool TreesAreDifferent(Block leftTree, Block rightTree, IEnumerable<TextChange> changes)
		{
			return BackgroundParser.TreesAreDifferent(leftTree, rightTree, changes, CancellationToken.None);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A10 File Offset: 0x00000C10
		internal static bool TreesAreDifferent(Block leftTree, Block rightTree, IEnumerable<TextChange> changes, CancellationToken cancelToken)
		{
			foreach (TextChange change in changes)
			{
				cancelToken.ThrowIfCancellationRequested();
				Span span = leftTree.LocateOwner(change);
				if (span == null)
				{
					return true;
				}
				EditResult editResult = span.EditHandler.ApplyChange(span, change, true);
				span.ReplaceWith(editResult.EditedSpan);
			}
			return !leftTree.EquivalentTo(rightTree);
		}

		// Token: 0x0400000E RID: 14
		private BackgroundParser.MainThreadState _main;

		// Token: 0x0400000F RID: 15
		private BackgroundParser.BackgroundThread _bg;

		// Token: 0x02000008 RID: 8
		private abstract class ThreadStateBase
		{
			// Token: 0x0600004E RID: 78 RVA: 0x00002AA0 File Offset: 0x00000CA0
			[Conditional("DEBUG")]
			protected void SetThreadId(int id)
			{
			}

			// Token: 0x0600004F RID: 79 RVA: 0x00002AA2 File Offset: 0x00000CA2
			[Conditional("DEBUG")]
			protected void EnsureOnThread()
			{
			}

			// Token: 0x06000050 RID: 80 RVA: 0x00002AA4 File Offset: 0x00000CA4
			[Conditional("DEBUG")]
			protected void EnsureNotOnThread()
			{
			}
		}

		// Token: 0x02000009 RID: 9
		private class MainThreadState : BackgroundParser.ThreadStateBase, IDisposable
		{
			// Token: 0x06000051 RID: 81 RVA: 0x00002AA6 File Offset: 0x00000CA6
			public MainThreadState(string fileName)
			{
				this._fileName = fileName;
			}

			// Token: 0x14000002 RID: 2
			// (add) Token: 0x06000052 RID: 82 RVA: 0x00002AE4 File Offset: 0x00000CE4
			// (remove) Token: 0x06000053 RID: 83 RVA: 0x00002B1C File Offset: 0x00000D1C
			public event EventHandler<DocumentParseCompleteEventArgs> ResultsReady;

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000054 RID: 84 RVA: 0x00002B51 File Offset: 0x00000D51
			public CancellationToken CancelToken
			{
				get
				{
					return this._cancelSource.Token;
				}
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000055 RID: 85 RVA: 0x00002B60 File Offset: 0x00000D60
			public bool IsIdle
			{
				get
				{
					bool result;
					lock (this._stateLock)
					{
						result = (this._currentParcelCancelSource == null);
					}
					return result;
				}
			}

			// Token: 0x06000056 RID: 86 RVA: 0x00002BA8 File Offset: 0x00000DA8
			public void Cancel()
			{
				this._cancelSource.Cancel();
			}

			// Token: 0x06000057 RID: 87 RVA: 0x00002BC2 File Offset: 0x00000DC2
			public IDisposable Lock()
			{
				Monitor.Enter(this._stateLock);
				return new DisposableAction(delegate()
				{
					Monitor.Exit(this._stateLock);
				});
			}

			// Token: 0x06000058 RID: 88 RVA: 0x00002BE0 File Offset: 0x00000DE0
			public void QueueChange(TextChange change)
			{
				lock (this._stateLock)
				{
					if (this._currentParcelCancelSource != null)
					{
						this._currentParcelCancelSource.Cancel();
					}
					this._changes.Add(change);
					this._hasParcel.Set();
				}
			}

			// Token: 0x06000059 RID: 89 RVA: 0x00002C44 File Offset: 0x00000E44
			public BackgroundParser.WorkParcel GetParcel()
			{
				this._hasParcel.Wait(this._cancelSource.Token);
				this._hasParcel.Reset();
				BackgroundParser.WorkParcel result;
				lock (this._stateLock)
				{
					this._currentParcelCancelSource = new CancellationTokenSource();
					IList<TextChange> changes = this._changes;
					this._changes = new List<TextChange>();
					result = new BackgroundParser.WorkParcel(changes, this._currentParcelCancelSource.Token);
				}
				return result;
			}

			// Token: 0x0600005A RID: 90 RVA: 0x00002CD0 File Offset: 0x00000ED0
			public void ReturnParcel(DocumentParseCompleteEventArgs args)
			{
				lock (this._stateLock)
				{
					if (this._currentParcelCancelSource != null)
					{
						this._currentParcelCancelSource.Dispose();
						this._currentParcelCancelSource = null;
					}
					if (this._changes.Any<TextChange>())
					{
						return;
					}
				}
				EventHandler<DocumentParseCompleteEventArgs> resultsReady = this.ResultsReady;
				if (resultsReady != null)
				{
					resultsReady(this, args);
				}
			}

			// Token: 0x0600005B RID: 91 RVA: 0x00002D48 File Offset: 0x00000F48
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x0600005C RID: 92 RVA: 0x00002D57 File Offset: 0x00000F57
			protected virtual void Dispose(bool disposing)
			{
				if (disposing)
				{
					if (this._currentParcelCancelSource != null)
					{
						this._currentParcelCancelSource.Dispose();
						this._currentParcelCancelSource = null;
					}
					this._cancelSource.Dispose();
					this._hasParcel.Dispose();
				}
			}

			// Token: 0x04000011 RID: 17
			private CancellationTokenSource _cancelSource = new CancellationTokenSource();

			// Token: 0x04000012 RID: 18
			private ManualResetEventSlim _hasParcel = new ManualResetEventSlim(false);

			// Token: 0x04000013 RID: 19
			private CancellationTokenSource _currentParcelCancelSource;

			// Token: 0x04000014 RID: 20
			private string _fileName;

			// Token: 0x04000015 RID: 21
			private object _stateLock = new object();

			// Token: 0x04000016 RID: 22
			private IList<TextChange> _changes = new List<TextChange>();
		}

		// Token: 0x0200000A RID: 10
		private class BackgroundThread : BackgroundParser.ThreadStateBase
		{
			// Token: 0x0600005E RID: 94 RVA: 0x00002D8C File Offset: 0x00000F8C
			public BackgroundThread(BackgroundParser.MainThreadState main, RazorEngineHost host, string fileName)
			{
				this._main = main;
				this._backgroundThread = new Thread(new ThreadStart(this.WorkerLoop));
				this._shutdownToken = this._main.CancelToken;
				this._host = host;
				this._fileName = fileName;
			}

			// Token: 0x0600005F RID: 95 RVA: 0x00002DE7 File Offset: 0x00000FE7
			public void Start()
			{
				this._backgroundThread.Start();
			}

			// Token: 0x06000060 RID: 96 RVA: 0x00002DF4 File Offset: 0x00000FF4
			private void WorkerLoop()
			{
				Path.GetFileName(this._fileName);
				try
				{
					while (!this._shutdownToken.IsCancellationRequested)
					{
						BackgroundParser.WorkParcel parcel = this._main.GetParcel();
						if (parcel.Changes.Any<TextChange>())
						{
							try
							{
								DocumentParseCompleteEventArgs documentParseCompleteEventArgs = null;
								using (CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(this._shutdownToken, parcel.CancelToken))
								{
									if (!cancellationTokenSource.IsCancellationRequested)
									{
										List<TextChange> list;
										if (this._previouslyDiscarded != null)
										{
											list = this._previouslyDiscarded.Concat(parcel.Changes).ToList<TextChange>();
										}
										else
										{
											list = parcel.Changes.ToList<TextChange>();
										}
										TextChange sourceChange = list.Last<TextChange>();
										GeneratorResults generatorResults = this.ParseChange(sourceChange.NewBuffer, cancellationTokenSource.Token);
										if (generatorResults != null && !cancellationTokenSource.IsCancellationRequested)
										{
											this._previouslyDiscarded = null;
											bool treeStructureChanged = this._currentParseTree == null || BackgroundParser.TreesAreDifferent(this._currentParseTree, generatorResults.Document, list, parcel.CancelToken);
											this._currentParseTree = generatorResults.Document;
											documentParseCompleteEventArgs = new DocumentParseCompleteEventArgs
											{
												GeneratorResults = generatorResults,
												SourceChange = sourceChange,
												TreeStructureChanged = treeStructureChanged
											};
										}
										else
										{
											this._previouslyDiscarded = list;
										}
									}
								}
								if (documentParseCompleteEventArgs != null)
								{
									this._main.ReturnParcel(documentParseCompleteEventArgs);
								}
								continue;
							}
							catch (OperationCanceledException)
							{
								continue;
							}
						}
						Thread.Yield();
					}
				}
				catch (OperationCanceledException)
				{
				}
				finally
				{
					this._main.Dispose();
				}
			}

			// Token: 0x06000061 RID: 97 RVA: 0x00002FB8 File Offset: 0x000011B8
			private GeneratorResults ParseChange(ITextBuffer buffer, CancellationToken token)
			{
				RazorTemplateEngine razorTemplateEngine = new RazorTemplateEngine(this._host);
				buffer.Position = 0;
				GeneratorResults result;
				try
				{
					result = razorTemplateEngine.GenerateCode(buffer, null, null, this._fileName, new CancellationToken?(token));
				}
				catch (OperationCanceledException)
				{
					result = null;
				}
				return result;
			}

			// Token: 0x04000018 RID: 24
			private BackgroundParser.MainThreadState _main;

			// Token: 0x04000019 RID: 25
			private Thread _backgroundThread;

			// Token: 0x0400001A RID: 26
			private CancellationToken _shutdownToken;

			// Token: 0x0400001B RID: 27
			private RazorEngineHost _host;

			// Token: 0x0400001C RID: 28
			private string _fileName;

			// Token: 0x0400001D RID: 29
			private Block _currentParseTree;

			// Token: 0x0400001E RID: 30
			private IList<TextChange> _previouslyDiscarded = new List<TextChange>();
		}

		// Token: 0x0200000B RID: 11
		private class WorkParcel
		{
			// Token: 0x06000062 RID: 98 RVA: 0x00003008 File Offset: 0x00001208
			public WorkParcel(IList<TextChange> changes, CancellationToken cancelToken)
			{
				this.Changes = changes;
				this.CancelToken = cancelToken;
			}

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000063 RID: 99 RVA: 0x0000301E File Offset: 0x0000121E
			// (set) Token: 0x06000064 RID: 100 RVA: 0x00003026 File Offset: 0x00001226
			public CancellationToken CancelToken { get; private set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000065 RID: 101 RVA: 0x0000302F File Offset: 0x0000122F
			// (set) Token: 0x06000066 RID: 102 RVA: 0x00003037 File Offset: 0x00001237
			public IList<TextChange> Changes { get; private set; }
		}
	}
}
