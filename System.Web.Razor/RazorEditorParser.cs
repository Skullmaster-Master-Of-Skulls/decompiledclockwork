using System;
using System.Diagnostics;
using System.Globalization;
using System.Web.Razor.Editor;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Resources;
using System.Web.Razor.Text;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor
{
	// Token: 0x02000052 RID: 82
	public class RazorEditorParser : IDisposable
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x00010B74 File Offset: 0x0000ED74
		public RazorEditorParser(RazorEngineHost host, string sourceFileName)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (string.IsNullOrEmpty(sourceFileName))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "sourceFileName");
			}
			this.Host = host;
			this.FileName = sourceFileName;
			this._parser = new BackgroundParser(host, sourceFileName);
			this._parser.ResultsReady += delegate(object sender, DocumentParseCompleteEventArgs args)
			{
				this.OnDocumentParseComplete(args);
			};
			this._parser.Start();
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060003C5 RID: 965 RVA: 0x00010BF4 File Offset: 0x0000EDF4
		// (remove) Token: 0x060003C6 RID: 966 RVA: 0x00010C2C File Offset: 0x0000EE2C
		public event EventHandler<DocumentParseCompleteEventArgs> DocumentParseComplete;

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00010C61 File Offset: 0x0000EE61
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x00010C69 File Offset: 0x0000EE69
		public RazorEngineHost Host { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00010C72 File Offset: 0x0000EE72
		// (set) Token: 0x060003CA RID: 970 RVA: 0x00010C7A File Offset: 0x0000EE7A
		public string FileName { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00010C83 File Offset: 0x0000EE83
		// (set) Token: 0x060003CC RID: 972 RVA: 0x00010C8B File Offset: 0x0000EE8B
		public bool LastResultProvisional { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00010C94 File Offset: 0x0000EE94
		public Block CurrentParseTree
		{
			get
			{
				return this._currentParseTree;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00010C9C File Offset: 0x0000EE9C
		public virtual string GetAutoCompleteString()
		{
			if (this._lastAutoCompleteSpan != null)
			{
				AutoCompleteEditHandler autoCompleteEditHandler = this._lastAutoCompleteSpan.EditHandler as AutoCompleteEditHandler;
				if (autoCompleteEditHandler != null)
				{
					return autoCompleteEditHandler.AutoCompleteString;
				}
			}
			return null;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00010CD0 File Offset: 0x0000EED0
		public virtual PartialParseResult CheckForStructureChanges(TextChange change)
		{
			if (change.NewBuffer == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, RazorResources.Structure_Member_CannotBeNull, new object[]
				{
					"Buffer",
					"TextChange"
				}), "change");
			}
			PartialParseResult partialParseResult = PartialParseResult.Rejected;
			string empty = string.Empty;
			using (this._parser.SynchronizeMainThreadState())
			{
				change.ToString();
				if (this.CurrentParseTree != null && this._parser.IsIdle)
				{
					partialParseResult = this.TryPartialParse(change);
				}
			}
			if (partialParseResult.HasFlag(PartialParseResult.Rejected))
			{
				this._parser.QueueChange(change);
			}
			this.LastResultProvisional = partialParseResult.HasFlag(PartialParseResult.Provisional);
			return partialParseResult;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00010DAC File Offset: 0x0000EFAC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00010DBB File Offset: 0x0000EFBB
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._parser.Dispose();
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00010DCC File Offset: 0x0000EFCC
		private PartialParseResult TryPartialParse(TextChange change)
		{
			PartialParseResult partialParseResult = PartialParseResult.Rejected;
			if (this._lastChangeOwner != null && this._lastChangeOwner.EditHandler.OwnsChange(this._lastChangeOwner, change))
			{
				EditResult editResult = this._lastChangeOwner.EditHandler.ApplyChange(this._lastChangeOwner, change);
				partialParseResult = editResult.Result;
				if (!editResult.Result.HasFlag(PartialParseResult.Rejected))
				{
					this._lastChangeOwner.ReplaceWith(editResult.EditedSpan);
				}
				return partialParseResult;
			}
			this._lastChangeOwner = this.CurrentParseTree.LocateOwner(change);
			if (this.LastResultProvisional)
			{
				partialParseResult = PartialParseResult.Rejected;
			}
			else if (this._lastChangeOwner != null)
			{
				EditResult editResult2 = this._lastChangeOwner.EditHandler.ApplyChange(this._lastChangeOwner, change);
				partialParseResult = editResult2.Result;
				if (!editResult2.Result.HasFlag(PartialParseResult.Rejected))
				{
					this._lastChangeOwner.ReplaceWith(editResult2.EditedSpan);
				}
				if (partialParseResult.HasFlag(PartialParseResult.AutoCompleteBlock))
				{
					this._lastAutoCompleteSpan = this._lastChangeOwner;
				}
				else
				{
					this._lastAutoCompleteSpan = null;
				}
			}
			return partialParseResult;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00010EE0 File Offset: 0x0000F0E0
		private void OnDocumentParseComplete(DocumentParseCompleteEventArgs args)
		{
			using (this._parser.SynchronizeMainThreadState())
			{
				this._currentParseTree = args.GeneratorResults.Document;
				this._lastChangeOwner = null;
			}
			EventHandler<DocumentParseCompleteEventArgs> documentParseComplete = this.DocumentParseComplete;
			if (documentParseComplete != null)
			{
				try
				{
					documentParseComplete(this, args);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00010F50 File Offset: 0x0000F150
		[Conditional("DEBUG")]
		private static void VerifyFlagsAreValid(PartialParseResult result)
		{
		}

		// Token: 0x04000106 RID: 262
		private Span _lastChangeOwner;

		// Token: 0x04000107 RID: 263
		private Span _lastAutoCompleteSpan;

		// Token: 0x04000108 RID: 264
		private BackgroundParser _parser;

		// Token: 0x04000109 RID: 265
		private Block _currentParseTree;
	}
}
