using System;
using System.ComponentModel;
using Telerik.Web.Data.Expressions;

namespace Telerik.Web.Data
{
	// Token: 0x02001B8B RID: 7051
	public class DescriptorBase : INotifyPropertyChanged
	{
		// Token: 0x140001E5 RID: 485
		// (add) Token: 0x0601115A RID: 69978 RVA: 0x003C4DBC File Offset: 0x003C2FBC
		// (remove) Token: 0x0601115B RID: 69979 RVA: 0x003C4DF4 File Offset: 0x003C2FF4
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0601115C RID: 69980 RVA: 0x003C4E2C File Offset: 0x003C302C
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, args);
			}
		}

		// Token: 0x0601115D RID: 69981 RVA: 0x003C4E4B File Offset: 0x003C304B
		protected void OnPropertyChanged(string propertyName)
		{
			this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x1700536F RID: 21359
		// (get) Token: 0x0601115E RID: 69982 RVA: 0x003C4E59 File Offset: 0x003C3059
		internal ExpressionBuilderOptions ExpressionBuilderOptions
		{
			get
			{
				if (this.options == null)
				{
					this.options = new ExpressionBuilderOptions();
				}
				return this.options;
			}
		}

		// Token: 0x04004C6F RID: 19567
		private ExpressionBuilderOptions options;
	}
}
