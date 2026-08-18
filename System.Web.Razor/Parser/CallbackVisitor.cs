using System;
using System.Threading;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Parser
{
	// Token: 0x02000036 RID: 54
	public class CallbackVisitor : ParserVisitor
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00007489 File Offset: 0x00005689
		public CallbackVisitor(Action<Span> spanCallback) : this(spanCallback, delegate(RazorError _)
		{
		})
		{
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000074B4 File Offset: 0x000056B4
		public CallbackVisitor(Action<Span> spanCallback, Action<RazorError> errorCallback) : this(spanCallback, errorCallback, delegate(BlockType _)
		{
		}, delegate(BlockType _)
		{
		})
		{
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00007505 File Offset: 0x00005705
		public CallbackVisitor(Action<Span> spanCallback, Action<RazorError> errorCallback, Action<BlockType> startBlockCallback, Action<BlockType> endBlockCallback) : this(spanCallback, errorCallback, startBlockCallback, endBlockCallback, delegate()
		{
		})
		{
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000752F File Offset: 0x0000572F
		public CallbackVisitor(Action<Span> spanCallback, Action<RazorError> errorCallback, Action<BlockType> startBlockCallback, Action<BlockType> endBlockCallback, Action completeCallback)
		{
			this._spanCallback = spanCallback;
			this._errorCallback = errorCallback;
			this._startBlockCallback = startBlockCallback;
			this._endBlockCallback = endBlockCallback;
			this._completeCallback = completeCallback;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000755C File Offset: 0x0000575C
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00007564 File Offset: 0x00005764
		public SynchronizationContext SynchronizationContext { get; set; }

		// Token: 0x06000207 RID: 519 RVA: 0x0000756D File Offset: 0x0000576D
		public override void VisitStartBlock(Block block)
		{
			base.VisitStartBlock(block);
			CallbackVisitor.RaiseCallback<BlockType>(this.SynchronizationContext, block.Type, this._startBlockCallback);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000758D File Offset: 0x0000578D
		public override void VisitSpan(Span span)
		{
			base.VisitSpan(span);
			CallbackVisitor.RaiseCallback<Span>(this.SynchronizationContext, span, this._spanCallback);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000075A8 File Offset: 0x000057A8
		public override void VisitEndBlock(Block block)
		{
			base.VisitEndBlock(block);
			CallbackVisitor.RaiseCallback<BlockType>(this.SynchronizationContext, block.Type, this._endBlockCallback);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000075C8 File Offset: 0x000057C8
		public override void VisitError(RazorError err)
		{
			base.VisitError(err);
			CallbackVisitor.RaiseCallback<RazorError>(this.SynchronizationContext, err, this._errorCallback);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000075F0 File Offset: 0x000057F0
		public override void OnComplete()
		{
			base.OnComplete();
			CallbackVisitor.RaiseCallback<object>(this.SynchronizationContext, null, delegate(object _)
			{
				this._completeCallback();
			});
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000762C File Offset: 0x0000582C
		private static void RaiseCallback<T>(SynchronizationContext syncContext, T param, Action<T> callback)
		{
			if (callback != null)
			{
				if (syncContext != null)
				{
					syncContext.Post(delegate(object state)
					{
						callback((T)((object)state));
					}, param);
					return;
				}
				callback(param);
			}
		}

		// Token: 0x04000090 RID: 144
		private Action<Span> _spanCallback;

		// Token: 0x04000091 RID: 145
		private Action<RazorError> _errorCallback;

		// Token: 0x04000092 RID: 146
		private Action<BlockType> _endBlockCallback;

		// Token: 0x04000093 RID: 147
		private Action<BlockType> _startBlockCallback;

		// Token: 0x04000094 RID: 148
		private Action _completeCallback;
	}
}
