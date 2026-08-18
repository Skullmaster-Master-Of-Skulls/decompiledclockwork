using System;

namespace System.Windows.Forms
{
	// Token: 0x02000425 RID: 1061
	public class TypeValidationEventArgs : EventArgs
	{
		// Token: 0x060049E1 RID: 18913 RVA: 0x0013752B File Offset: 0x0013572B
		public TypeValidationEventArgs(Type validatingType, bool isValidInput, object returnValue, string message)
		{
			this.validatingType = validatingType;
			this.isValidInput = isValidInput;
			this.returnValue = returnValue;
			this.message = message;
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x060049E2 RID: 18914 RVA: 0x00137550 File Offset: 0x00135750
		// (set) Token: 0x060049E3 RID: 18915 RVA: 0x00137558 File Offset: 0x00135758
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
			}
		}

		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x060049E4 RID: 18916 RVA: 0x00137561 File Offset: 0x00135761
		public bool IsValidInput
		{
			get
			{
				return this.isValidInput;
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x060049E5 RID: 18917 RVA: 0x00137569 File Offset: 0x00135769
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x060049E6 RID: 18918 RVA: 0x00137571 File Offset: 0x00135771
		public object ReturnValue
		{
			get
			{
				return this.returnValue;
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x060049E7 RID: 18919 RVA: 0x00137579 File Offset: 0x00135779
		public Type ValidatingType
		{
			get
			{
				return this.validatingType;
			}
		}

		// Token: 0x040027BA RID: 10170
		private Type validatingType;

		// Token: 0x040027BB RID: 10171
		private string message;

		// Token: 0x040027BC RID: 10172
		private bool isValidInput;

		// Token: 0x040027BD RID: 10173
		private object returnValue;

		// Token: 0x040027BE RID: 10174
		private bool cancel;
	}
}
