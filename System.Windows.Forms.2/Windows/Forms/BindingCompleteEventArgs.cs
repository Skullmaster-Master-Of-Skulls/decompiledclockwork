using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000132 RID: 306
	public class BindingCompleteEventArgs : CancelEventArgs
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x0001F7C1 File Offset: 0x0001D9C1
		public BindingCompleteEventArgs(Binding binding, BindingCompleteState state, BindingCompleteContext context, string errorText, Exception exception, bool cancel) : base(cancel)
		{
			this.binding = binding;
			this.state = state;
			this.context = context;
			this.errorText = ((errorText == null) ? string.Empty : errorText);
			this.exception = exception;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0001F7FB File Offset: 0x0001D9FB
		public BindingCompleteEventArgs(Binding binding, BindingCompleteState state, BindingCompleteContext context, string errorText, Exception exception) : this(binding, state, context, errorText, exception, true)
		{
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0001F80B File Offset: 0x0001DA0B
		public BindingCompleteEventArgs(Binding binding, BindingCompleteState state, BindingCompleteContext context, string errorText) : this(binding, state, context, errorText, null, true)
		{
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0001F81A File Offset: 0x0001DA1A
		public BindingCompleteEventArgs(Binding binding, BindingCompleteState state, BindingCompleteContext context) : this(binding, state, context, string.Empty, null, false)
		{
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x0001F82C File Offset: 0x0001DA2C
		public Binding Binding
		{
			get
			{
				return this.binding;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0001F834 File Offset: 0x0001DA34
		public BindingCompleteState BindingCompleteState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x0001F83C File Offset: 0x0001DA3C
		public BindingCompleteContext BindingCompleteContext
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0001F844 File Offset: 0x0001DA44
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0001F84C File Offset: 0x0001DA4C
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x040006BA RID: 1722
		private Binding binding;

		// Token: 0x040006BB RID: 1723
		private BindingCompleteState state;

		// Token: 0x040006BC RID: 1724
		private BindingCompleteContext context;

		// Token: 0x040006BD RID: 1725
		private string errorText;

		// Token: 0x040006BE RID: 1726
		private Exception exception;
	}
}
