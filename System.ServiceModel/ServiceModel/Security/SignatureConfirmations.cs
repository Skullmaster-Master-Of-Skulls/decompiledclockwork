using System;

namespace System.ServiceModel.Security
{
	// Token: 0x020002B9 RID: 697
	internal class SignatureConfirmations
	{
		// Token: 0x0600160D RID: 5645 RVA: 0x00053DD9 File Offset: 0x00051FD9
		public SignatureConfirmations()
		{
			this.confirmations = new SignatureConfirmations.SignatureConfirmation[1];
			this.length = 0;
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x00053DF4 File Offset: 0x00051FF4
		public int Count
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00053DFC File Offset: 0x00051FFC
		public void AddConfirmation(byte[] value, bool encrypted)
		{
			if (this.confirmations.Length == this.length)
			{
				SignatureConfirmations.SignatureConfirmation[] destinationArray = new SignatureConfirmations.SignatureConfirmation[this.length * 2];
				Array.Copy(this.confirmations, 0, destinationArray, 0, this.length);
				this.confirmations = destinationArray;
			}
			this.confirmations[this.length] = new SignatureConfirmations.SignatureConfirmation(value);
			this.length++;
			this.encrypted = (this.encrypted || encrypted);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00053E78 File Offset: 0x00052078
		public void GetConfirmation(int index, out byte[] value, out bool encrypted)
		{
			if (index < 0 || index >= this.length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					this.length
				})));
			}
			value = this.confirmations[index].value;
			encrypted = this.encrypted;
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x00053EE9 File Offset: 0x000520E9
		public bool IsMarkedForEncryption
		{
			get
			{
				return this.encrypted;
			}
		}

		// Token: 0x04001BA8 RID: 7080
		private SignatureConfirmations.SignatureConfirmation[] confirmations;

		// Token: 0x04001BA9 RID: 7081
		private int length;

		// Token: 0x04001BAA RID: 7082
		private bool encrypted;

		// Token: 0x02000B48 RID: 2888
		private struct SignatureConfirmation
		{
			// Token: 0x060070E1 RID: 28897 RVA: 0x001A4312 File Offset: 0x001A2512
			public SignatureConfirmation(byte[] value)
			{
				this.value = value;
			}

			// Token: 0x04004032 RID: 16434
			public byte[] value;
		}
	}
}
