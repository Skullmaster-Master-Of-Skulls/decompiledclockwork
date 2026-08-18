using System;
using System.IO;
using System.Text;

namespace System.Web.UI
{
	// Token: 0x020002C2 RID: 706
	public sealed class LosFormatter
	{
		// Token: 0x06001FEF RID: 8175 RVA: 0x00065968 File Offset: 0x00063B68
		public LosFormatter() : this(false, null)
		{
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x00065972 File Offset: 0x00063B72
		public LosFormatter(bool enableMac, string macKeyModifier) : this(enableMac, LosFormatter.GetBytes(macKeyModifier))
		{
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x00065981 File Offset: 0x00063B81
		public LosFormatter(bool enableMac, byte[] macKeyModifier)
		{
			this._enableMac = enableMac;
			if (enableMac)
			{
				this._formatter = new ObjectStateFormatter(macKeyModifier);
				return;
			}
			this._formatter = new ObjectStateFormatter();
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000659AB File Offset: 0x00063BAB
		private static byte[] GetBytes(string s)
		{
			if (s != null && s.Length != 0)
			{
				return Encoding.Unicode.GetBytes(s);
			}
			return null;
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x000659C8 File Offset: 0x00063BC8
		public object Deserialize(Stream stream)
		{
			TextReader input = new StreamReader(stream);
			return this.Deserialize(input);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x000659E8 File Offset: 0x00063BE8
		public object Deserialize(TextReader input)
		{
			char[] array = new char[128];
			int num = 0;
			int num2 = 24;
			int num3;
			do
			{
				num3 = input.Read(array, num, num2);
				num += num3;
				if (num > array.Length - num2)
				{
					char[] array2 = new char[array.Length * 2];
					Array.Copy(array, array2, array.Length);
					array = array2;
				}
			}
			while (num3 == num2);
			return this.Deserialize(new string(array, 0, num));
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x00065A49 File Offset: 0x00063C49
		public object Deserialize(string input)
		{
			return this._formatter.Deserialize(input);
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x00065A58 File Offset: 0x00063C58
		public void Serialize(Stream stream, object value)
		{
			TextWriter textWriter = new StreamWriter(stream);
			this.SerializeInternal(textWriter, value);
			textWriter.Flush();
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x00065A7A File Offset: 0x00063C7A
		public void Serialize(TextWriter output, object value)
		{
			this.SerializeInternal(output, value);
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x00065A84 File Offset: 0x00063C84
		private void SerializeInternal(TextWriter output, object value)
		{
			string value2 = this._formatter.Serialize(value);
			output.Write(value2);
		}

		// Token: 0x04001ABF RID: 6847
		private const int InitialBufferSize = 24;

		// Token: 0x04001AC0 RID: 6848
		private ObjectStateFormatter _formatter;

		// Token: 0x04001AC1 RID: 6849
		private bool _enableMac;
	}
}
