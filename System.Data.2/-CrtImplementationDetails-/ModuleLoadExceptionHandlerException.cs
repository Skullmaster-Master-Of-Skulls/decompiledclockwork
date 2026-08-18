using System;
using System.Runtime.Serialization;
using System.Security;

namespace <CrtImplementationDetails>
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	internal class ModuleLoadExceptionHandlerException : ModuleLoadException
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00005718 File Offset: 0x00004B18
		protected ModuleLoadExceptionHandlerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			Type typeFromHandle = typeof(Exception);
			string name = "NestedException";
			this.NestedException = (Exception)info.GetValue(name, typeFromHandle);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005C84 File Offset: 0x00005084
		public ModuleLoadExceptionHandlerException(string message, Exception innerException, Exception nestedException) : base(message, innerException)
		{
			this.NestedException = nestedException;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00005614 File Offset: 0x00004A14
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00005628 File Offset: 0x00004A28
		public Exception NestedException
		{
			get
			{
				return this.<backing_store>NestedException;
			}
			set
			{
				this.<backing_store>NestedException = value;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000563C File Offset: 0x00004A3C
		public override string ToString()
		{
			string text;
			if (this.InnerException != null)
			{
				text = this.InnerException.ToString();
			}
			else
			{
				text = string.Empty;
			}
			string text2;
			if (this.NestedException != null)
			{
				text2 = this.NestedException.ToString();
			}
			else
			{
				text2 = string.Empty;
			}
			object[] array = new object[4];
			Type type = this.GetType();
			array[0] = type;
			string text3;
			if (this.Message != null)
			{
				text3 = this.Message;
			}
			else
			{
				text3 = string.Empty;
			}
			array[1] = text3;
			string text4;
			if (text != null)
			{
				text4 = text;
			}
			else
			{
				text4 = string.Empty;
			}
			array[2] = text4;
			string text5;
			if (text2 != null)
			{
				text5 = text2;
			}
			else
			{
				text5 = string.Empty;
			}
			array[3] = text5;
			return string.Format("\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n", array);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000056E4 File Offset: 0x00004AE4
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			Type typeFromHandle = typeof(Exception);
			Exception nestedException = this.NestedException;
			info.AddValue("NestedException", nestedException, typeFromHandle);
		}

		// Token: 0x040000A2 RID: 162
		private const string formatString = "\n{0}: {1}\n--- Start of primary exception ---\n{2}\n--- End of primary exception ---\n\n--- Start of nested exception ---\n{3}\n--- End of nested exception ---\n";

		// Token: 0x040000A3 RID: 163
		private Exception <backing_store>NestedException;
	}
}
