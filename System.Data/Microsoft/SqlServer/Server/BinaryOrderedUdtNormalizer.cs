using System;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200028A RID: 650
	internal sealed class BinaryOrderedUdtNormalizer : Normalizer
	{
		// Token: 0x0600221E RID: 8734 RVA: 0x0028AE68 File Offset: 0x0028A268
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private FieldInfo[] GetFields(Type t)
		{
			return t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x0028AE88 File Offset: 0x0028A288
		internal BinaryOrderedUdtNormalizer(Type t, bool isTopLevelUdt)
		{
			this.m_skipNormalize = false;
			if (this.m_skipNormalize)
			{
				this.m_isTopLevelUdt = true;
			}
			this.m_isTopLevelUdt = true;
			FieldInfo[] fields = this.GetFields(t);
			this.m_fieldsToNormalize = new FieldInfoEx[fields.Length];
			int num = 0;
			foreach (FieldInfo fieldInfo in fields)
			{
				int offset = Marshal.OffsetOf(fieldInfo.DeclaringType, fieldInfo.Name).ToInt32();
				this.m_fieldsToNormalize[num++] = new FieldInfoEx(fieldInfo, offset, Normalizer.GetNormalizer(fieldInfo.FieldType));
			}
			Array.Sort<FieldInfoEx>(this.m_fieldsToNormalize);
			if (!this.m_isTopLevelUdt && typeof(INullable).IsAssignableFrom(t))
			{
				PropertyInfo property = t.GetProperty("Null", BindingFlags.Static | BindingFlags.Public);
				if (property == null || property.PropertyType != t)
				{
					FieldInfo field = t.GetField("Null", BindingFlags.Static | BindingFlags.Public);
					if (field == null || field.FieldType != t)
					{
						throw new Exception("could not find Null field/property in nullable type " + t);
					}
					this.NullInstance = field.GetValue(null);
				}
				else
				{
					this.NullInstance = property.GetValue(null, null);
				}
				this.m_PadBuffer = new byte[this.Size - 1];
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x0028AFC8 File Offset: 0x0028A3C8
		internal bool IsNullable
		{
			get
			{
				return this.NullInstance != null;
			}
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0028AFE8 File Offset: 0x0028A3E8
		internal void NormalizeTopObject(object udt, Stream s)
		{
			this.Normalize(null, udt, s);
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0028B008 File Offset: 0x0028A408
		internal object DeNormalizeTopObject(Type t, Stream s)
		{
			return this.DeNormalizeInternal(t, s);
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x0028B028 File Offset: 0x0028A428
		private object DeNormalizeInternal(Type t, Stream s)
		{
			object obj = null;
			if (!this.m_isTopLevelUdt && typeof(INullable).IsAssignableFrom(t) && (byte)s.ReadByte() == 0)
			{
				obj = this.NullInstance;
				s.Read(this.m_PadBuffer, 0, this.m_PadBuffer.Length);
				return obj;
			}
			if (obj == null)
			{
				obj = Activator.CreateInstance(t);
			}
			foreach (FieldInfoEx fieldInfoEx in this.m_fieldsToNormalize)
			{
				fieldInfoEx.normalizer.DeNormalize(fieldInfoEx.fieldInfo, obj, s);
			}
			return obj;
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x0028B0B8 File Offset: 0x0028A4B8
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			object obj2;
			if (fi == null)
			{
				obj2 = obj;
			}
			else
			{
				obj2 = base.GetValue(fi, obj);
			}
			INullable nullable = obj2 as INullable;
			if (nullable != null && !this.m_isTopLevelUdt)
			{
				if (nullable.IsNull)
				{
					s.WriteByte(0);
					s.Write(this.m_PadBuffer, 0, this.m_PadBuffer.Length);
					return;
				}
				s.WriteByte(1);
			}
			foreach (FieldInfoEx fieldInfoEx in this.m_fieldsToNormalize)
			{
				fieldInfoEx.normalizer.Normalize(fieldInfoEx.fieldInfo, obj2, s);
			}
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x0028B148 File Offset: 0x0028A548
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			base.SetValue(fi, recvr, this.DeNormalizeInternal(fi.FieldType, s));
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x0028B178 File Offset: 0x0028A578
		internal override int Size
		{
			get
			{
				if (this.m_size != 0)
				{
					return this.m_size;
				}
				if (this.IsNullable && !this.m_isTopLevelUdt)
				{
					this.m_size = 1;
				}
				foreach (FieldInfoEx fieldInfoEx in this.m_fieldsToNormalize)
				{
					this.m_size += fieldInfoEx.normalizer.Size;
				}
				return this.m_size;
			}
		}

		// Token: 0x0400165B RID: 5723
		internal readonly FieldInfoEx[] m_fieldsToNormalize;

		// Token: 0x0400165C RID: 5724
		private int m_size;

		// Token: 0x0400165D RID: 5725
		private byte[] m_PadBuffer;

		// Token: 0x0400165E RID: 5726
		internal readonly object NullInstance;

		// Token: 0x0400165F RID: 5727
		private bool m_isTopLevelUdt;
	}
}
