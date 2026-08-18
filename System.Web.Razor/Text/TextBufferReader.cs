using System;
using System.Collections.Generic;
using System.Web.Razor.Resources;
using System.Web.Razor.Utils;

namespace System.Web.Razor.Text
{
	// Token: 0x02000069 RID: 105
	public class TextBufferReader : LookaheadTextReader
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x00012757 File Offset: 0x00010957
		public TextBufferReader(ITextBuffer buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.InnerBuffer = buffer;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0001278A File Offset: 0x0001098A
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x00012792 File Offset: 0x00010992
		internal ITextBuffer InnerBuffer { get; private set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0001279B File Offset: 0x0001099B
		public override SourceLocation CurrentLocation
		{
			get
			{
				return this._tracker.CurrentLocation;
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000127A8 File Offset: 0x000109A8
		public override int Peek()
		{
			return this.InnerBuffer.Peek();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000127B8 File Offset: 0x000109B8
		public override int Read()
		{
			int num = this.InnerBuffer.Read();
			if (num != -1)
			{
				char nextCharacter = '\0';
				int num2 = this.Peek();
				if (num2 != -1)
				{
					nextCharacter = (char)num2;
				}
				this._tracker.UpdateLocation((char)num, nextCharacter);
			}
			return num;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000127F4 File Offset: 0x000109F4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				IDisposable disposable = this.InnerBuffer as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001283C File Offset: 0x00010A3C
		public override IDisposable BeginLookahead()
		{
			TextBufferReader.BacktrackContext context = new TextBufferReader.BacktrackContext
			{
				Location = this.CurrentLocation
			};
			this._bookmarks.Push(context);
			return new DisposableAction(delegate()
			{
				this.EndLookahead(context);
			});
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00012891 File Offset: 0x00010A91
		public override void CancelBacktrack()
		{
			if (this._bookmarks.Count == 0)
			{
				throw new InvalidOperationException(RazorResources.CancelBacktrack_Must_Be_Called_Within_Lookahead);
			}
			this._bookmarks.Pop();
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000128B8 File Offset: 0x00010AB8
		private void EndLookahead(TextBufferReader.BacktrackContext context)
		{
			if (this._bookmarks.Count > 0 && object.ReferenceEquals(this._bookmarks.Peek(), context))
			{
				this._bookmarks.Pop();
				this._tracker.CurrentLocation = context.Location;
				this.InnerBuffer.Position = context.Location.AbsoluteIndex;
			}
		}

		// Token: 0x04000153 RID: 339
		private Stack<TextBufferReader.BacktrackContext> _bookmarks = new Stack<TextBufferReader.BacktrackContext>();

		// Token: 0x04000154 RID: 340
		private SourceLocationTracker _tracker = new SourceLocationTracker();

		// Token: 0x0200006A RID: 106
		private class BacktrackContext
		{
			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0001291C File Offset: 0x00010B1C
			// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00012924 File Offset: 0x00010B24
			public SourceLocation Location { get; set; }
		}
	}
}
