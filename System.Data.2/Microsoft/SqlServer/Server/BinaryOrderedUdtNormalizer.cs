using System;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200005E RID: 94
	internal sealed class BinaryOrderedUdtNormalizer : Normalizer
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x000466DC File Offset: 0x00045ADC
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private FieldInfo[] GetFields(Type t)
		{
			return t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000466F4 File Offset: 0x00045AF4
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
						throw new Exception("could not find Null field/property in nullable type " + ((t != null) ? t.ToString() : null));
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00046858 File Offset: 0x00045C58
		internal bool IsNullable
		{
			get
			{
				return this.NullInstance != null;
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00046870 File Offset: 0x00045C70
		internal void NormalizeTopObject(object udt, Stream s)
		{
			this.Normalize(null, udt, s);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00046888 File Offset: 0x00045C88
		internal object DeNormalizeTopObject(Type t, Stream s)
		{
			return this.DeNormalizeInternal(t, s);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000468A0 File Offset: 0x00045CA0
		[MethodImpl(MethodImplOptions.NoInlining)]
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

		// Token: 0x060004F1 RID: 1265 RVA: 0x0004692C File Offset: 0x00045D2C
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

		// Token: 0x060004F2 RID: 1266 RVA: 0x000469BC File Offset: 0x00045DBC
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			base.SetValue(fi, recvr, this.DeNormalizeInternal(fi.FieldType, s));
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x000469E0 File Offset: 0x00045DE0
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

		// Token: 0x040001E1 RID: 481
		internal readonly FieldInfoEx[] m_fieldsToNormalize;

		// Token: 0x040001E2 RID: 482
		private int m_size;

		// Token: 0x040001E3 RID: 483
		private byte[] m_PadBuffer;

		// Token: 0x040001E4 RID: 484
		internal readonly object NullInstance;

		// Token: 0x040001E5 RID: 485
		private bool m_isTopLevelUdt;
	}
}
