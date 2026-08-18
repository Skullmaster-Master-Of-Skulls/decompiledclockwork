using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007D5 RID: 2005
	internal static class ConnectionUtilities
	{
		// Token: 0x06004BAB RID: 19371 RVA: 0x001144D4 File Offset: 0x001126D4
		internal static void CloseNoThrow(IConnection connection, TimeSpan timeout)
		{
			bool flag = false;
			try
			{
				connection.Close(timeout, false);
				flag = true;
			}
			catch (TimeoutException ex)
			{
				if (TD.CloseTimeoutIsEnabled())
				{
					TD.CloseTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			finally
			{
				if (!flag)
				{
					connection.Abort();
				}
			}
		}

		// Token: 0x06004BAC RID: 19372 RVA: 0x00114548 File Offset: 0x00112748
		internal static void ValidateBufferBounds(ArraySegment<byte> buffer)
		{
			ConnectionUtilities.ValidateBufferBounds(buffer.Array, buffer.Offset, buffer.Count);
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x00114564 File Offset: 0x00112764
		internal static void ValidateBufferBounds(byte[] buffer, int offset, int size)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
			}
			ConnectionUtilities.ValidateBufferBounds(buffer.Length, offset, size);
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x00114584 File Offset: 0x00112784
		internal static void ValidateBufferBounds(int bufferSize, int offset, int size)
		{
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", offset, SR.GetString("ValueMustBeNonNegative")));
			}
			if (offset > bufferSize)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", offset, SR.GetString("OffsetExceedsBufferSize", new object[]
				{
					bufferSize
				})));
			}
			if (size <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("size", size, SR.GetString("ValueMustBePositive")));
			}
			int num = bufferSize - offset;
			if (size > num)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("size", size, SR.GetString("SizeExceedsRemainingBufferSpace", new object[]
				{
					num
				})));
			}
		}
	}
}
