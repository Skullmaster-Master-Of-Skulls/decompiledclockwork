using System;
using System.Runtime.Serialization;

namespace System.Net.Http
{
	// Token: 0x0200000F RID: 15
	[__DynamicallyInvokable]
	[Serializable]
	public class HttpRequestException : Exception
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x0000470C File Offset: 0x0000290C
		[__DynamicallyInvokable]
		public HttpRequestException() : this(null, null)
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004716 File Offset: 0x00002916
		[__DynamicallyInvokable]
		public HttpRequestException(string message) : this(message, null)
		{
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004720 File Offset: 0x00002920
		[__DynamicallyInvokable]
		public HttpRequestException(string message, Exception inner) : base(message, inner)
		{
			base.SerializeObjectState += HttpRequestException.handleSerialization;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004735 File Offset: 0x00002935
		private static void HandleSerialization(object exception, SafeSerializationEventArgs eventArgs)
		{
			eventArgs.AddSerializedState(new HttpRequestException.EmptyState());
		}

		// Token: 0x0400008B RID: 139
		private static readonly EventHandler<SafeSerializationEventArgs> handleSerialization = new EventHandler<SafeSerializationEventArgs>(HttpRequestException.HandleSerialization);

		// Token: 0x02000054 RID: 84
		[Serializable]
		private class EmptyState : ISafeSerializationData
		{
			// Token: 0x06000429 RID: 1065 RVA: 0x0000F810 File Offset: 0x0000DA10
			public void CompleteDeserialization(object deserialized)
			{
				HttpRequestException ex = (HttpRequestException)deserialized;
				ex.SerializeObjectState += HttpRequestException.handleSerialization;
			}
		}
	}
}
