using System;
using System.IO;
using System.Reflection;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000153 RID: 339
	internal class ClrTypeAnnotationSerializer : IMetadataAnnotationSerializer
	{
		// Token: 0x06000B17 RID: 2839 RVA: 0x00037E54 File Offset: 0x00036054
		public string Serialize(string name, object value)
		{
			return ((Type)value).AssemblyQualifiedName;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00037E64 File Offset: 0x00036064
		public object Deserialize(string name, string value)
		{
			try
			{
				return Type.GetType(value, false);
			}
			catch (FileLoadException)
			{
			}
			catch (TargetInvocationException)
			{
			}
			catch (BadImageFormatException)
			{
			}
			return null;
		}
	}
}
