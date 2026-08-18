using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CDD RID: 3293
	public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
	{
		// Token: 0x1400012E RID: 302
		// (add) Token: 0x06007B17 RID: 31511 RVA: 0x001C43AC File Offset: 0x001C25AC
		// (remove) Token: 0x06007B18 RID: 31512 RVA: 0x001C43E4 File Offset: 0x001C25E4
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06007B19 RID: 31513 RVA: 0x001C4419 File Offset: 0x001C2619
		[DebuggerStepThrough]
		[Conditional("DEBUG")]
		protected void VerifyPropertyName(string propertyName)
		{
			base.GetType().GetProperty(propertyName) == null;
		}

		// Token: 0x06007B1A RID: 31514 RVA: 0x001C442E File Offset: 0x001C262E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06007B1B RID: 31515 RVA: 0x001C443D File Offset: 0x001C263D
		internal void RaisePropertyChanged(string propertyName)
		{
			this.OnPropertyChanged(propertyName);
		}

		// Token: 0x06007B1C RID: 31516 RVA: 0x001C4448 File Offset: 0x001C2648
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
				propertyChanged(this, e);
			}
		}

		// Token: 0x06007B1D RID: 31517 RVA: 0x001C446E File Offset: 0x001C266E
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
