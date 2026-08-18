using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007D7 RID: 2007
	internal sealed class BinaryAssemblyInfo
	{
		// Token: 0x0600472D RID: 18221 RVA: 0x000F3ABF File Offset: 0x000F2ABF
		internal BinaryAssemblyInfo(string assemblyString)
		{
			this.assemblyString = assemblyString;
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x000F3ACE File Offset: 0x000F2ACE
		internal BinaryAssemblyInfo(string assemblyString, Assembly assembly)
		{
			this.assemblyString = assemblyString;
			this.assembly = assembly;
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x000F3AE4 File Offset: 0x000F2AE4
		internal Assembly GetAssembly()
		{
			if (this.assembly == null)
			{
				this.assembly = FormatterServices.LoadAssemblyFromStringNoThrow(this.assemblyString);
				if (this.assembly == null)
				{
					throw new SerializationException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Serialization_AssemblyNotFound"), new object[]
					{
						this.assemblyString
					}));
				}
			}
			return this.assembly;
		}

		// Token: 0x040023EF RID: 9199
		internal string assemblyString;

		// Token: 0x040023F0 RID: 9200
		private Assembly assembly;
	}
}
