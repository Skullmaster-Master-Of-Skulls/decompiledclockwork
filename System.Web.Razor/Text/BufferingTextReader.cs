using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Razor.Resources;
using System.Web.Razor.Utils;

namespace System.Web.Razor.Text
{
	// Token: 0x0200005E RID: 94
	public class BufferingTextReader : LookaheadTextReader
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x00011C42 File Offset: 0x0000FE42
		public BufferingTextReader(TextReader source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.InnerReader = source;
			this._locationTracker = new SourceLocationTracker();
			this.UpdateCurrentCharacter();
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00011C7B File Offset: 0x0000FE7B
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x00011C83 File Offset: 0x0000FE83
		internal StringBuilder Buffer { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00011C8C File Offset: 0x0000FE8C
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x00011C94 File Offset: 0x0000FE94
		internal bool Buffering { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00011C9D File Offset: 0x0000FE9D
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x00011CA5 File Offset: 0x0000FEA5
		internal TextReader InnerReader { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00011CAE File Offset: 0x0000FEAE
		public override SourceLocation CurrentLocation
		{
			get
			{
				return this._locationTracker.CurrentLocation;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00011CBB File Offset: 0x0000FEBB
		protected virtual int CurrentCharacter
		{
			get
			{
				return this._currentCharacter;
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00011CC4 File Offset: 0x0000FEC4
		public override int Read()
		{
			int currentCharacter = this.CurrentCharacter;
			this.NextCharacter();
			return currentCharacter;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00011CDF File Offset: 0x0000FEDF
		public override int Peek()
		{
			return this.CurrentCharacter;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00011CE7 File Offset: 0x0000FEE7
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.InnerReader.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00011D1C File Offset: 0x0000FF1C
		public override IDisposable BeginLookahead()
		{
			if (this.Buffer == null)
			{
				this.Buffer = new StringBuilder();
			}
			if (!this.Buffering)
			{
				this.ExpandBuffer();
				this.Buffering = true;
			}
			BufferingTextReader.BacktrackContext context = new BufferingTextReader.BacktrackContext
			{
				BufferIndex = this._currentBufferPosition,
				Location = this.CurrentLocation
			};
			this._backtrackStack.Push(context);
			return new DisposableAction(delegate()
			{
				this.EndLookahead(context);
			});
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00011DA6 File Offset: 0x0000FFA6
		public override void CancelBacktrack()
		{
			if (this._backtrackStack.Count == 0)
			{
				throw new InvalidOperationException(RazorResources.CancelBacktrack_Must_Be_Called_Within_Lookahead);
			}
			this._backtrackStack.Pop();
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00011DCC File Offset: 0x0000FFCC
		private void EndLookahead(BufferingTextReader.BacktrackContext context)
		{
			if (this._backtrackStack.Count > 0 && object.ReferenceEquals(this._backtrackStack.Peek(), context))
			{
				this._backtrackStack.Pop();
				this._currentBufferPosition = context.BufferIndex;
				this._locationTracker.CurrentLocation = context.Location;
				this.UpdateCurrentCharacter();
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00011E2C File Offset: 0x0001002C
		protected virtual void NextCharacter()
		{
			int currentCharacter = this.CurrentCharacter;
			if (currentCharacter == -1)
			{
				return;
			}
			if (this.Buffering)
			{
				if (this._currentBufferPosition >= this.Buffer.Length - 1)
				{
					if (this._backtrackStack.Count == 0)
					{
						this.Buffer.Length = 0;
						this._currentBufferPosition = 0;
						this.Buffering = false;
					}
					else if (!this.ExpandBuffer())
					{
						this._currentBufferPosition = this.Buffer.Length;
					}
				}
				else
				{
					this._currentBufferPosition++;
				}
			}
			else
			{
				this.InnerReader.Read();
			}
			this.UpdateCurrentCharacter();
			this._locationTracker.UpdateLocation((char)currentCharacter, (char)this.CurrentCharacter);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00011EDC File Offset: 0x000100DC
		protected bool ExpandBuffer()
		{
			int num = this.InnerReader.Read();
			if (num != -1)
			{
				this.Buffer.Append((char)num);
				this._currentBufferPosition = this.Buffer.Length - 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00011F20 File Offset: 0x00010120
		private void UpdateCurrentCharacter()
		{
			if (this.Buffering && this._currentBufferPosition < this.Buffer.Length)
			{
				this._currentCharacter = (int)this.Buffer[this._currentBufferPosition];
				return;
			}
			this._currentCharacter = this.InnerReader.Peek();
		}

		// Token: 0x0400013A RID: 314
		private Stack<BufferingTextReader.BacktrackContext> _backtrackStack = new Stack<BufferingTextReader.BacktrackContext>();

		// Token: 0x0400013B RID: 315
		private int _currentBufferPosition;

		// Token: 0x0400013C RID: 316
		private int _currentCharacter;

		// Token: 0x0400013D RID: 317
		private SourceLocationTracker _locationTracker;

		// Token: 0x0200005F RID: 95
		private class BacktrackContext
		{
			// Token: 0x170000AF RID: 175
			// (get) Token: 0x06000476 RID: 1142 RVA: 0x00011F71 File Offset: 0x00010171
			// (set) Token: 0x06000477 RID: 1143 RVA: 0x00011F79 File Offset: 0x00010179
			public int BufferIndex { get; set; }

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x06000478 RID: 1144 RVA: 0x00011F82 File Offset: 0x00010182
			// (set) Token: 0x06000479 RID: 1145 RVA: 0x00011F8A File Offset: 0x0001018A
			public SourceLocation Location { get; set; }
		}
	}
}
