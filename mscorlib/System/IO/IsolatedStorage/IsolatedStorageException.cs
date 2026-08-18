using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO.IsolatedStorage
{
	// Token: 0x020007B1 RID: 1969
	[ComVisible(true)]
	[Serializable]
	public class IsolatedStorageException : Exception
	{
		// Token: 0x06004663 RID: 18019 RVA: 0x000F0754 File Offset: 0x000EF754
		public IsolatedStorageException() : base(Environment.GetResourceString("IsolatedStorage_Exception"))
		{
			base.SetErrorCode(-2146233264);
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x000F0771 File Offset: 0x000EF771
		public IsolatedStorageException(string message) : base(message)
		{
			base.SetErrorCode(-2146233264);
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x000F0785 File Offset: 0x000EF785
		public IsolatedStorageException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233264);
		}

		// Token: 0x06004666 RID: 18022 RVA: 0x000F079A File Offset: 0x000EF79A
		protected IsolatedStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
