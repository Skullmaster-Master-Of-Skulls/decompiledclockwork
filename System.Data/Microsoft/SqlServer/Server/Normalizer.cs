using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Security.Permissions;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000289 RID: 649
	internal abstract class Normalizer
	{
		// Token: 0x06002216 RID: 8726 RVA: 0x0028AC88 File Offset: 0x0028A088
		internal static Normalizer GetNormalizer(Type t)
		{
			Normalizer normalizer = null;
			if (t.IsPrimitive)
			{
				if (t == typeof(byte))
				{
					normalizer = new ByteNormalizer();
				}
				else if (t == typeof(sbyte))
				{
					normalizer = new SByteNormalizer();
				}
				else if (t == typeof(bool))
				{
					normalizer = new BooleanNormalizer();
				}
				else if (t == typeof(short))
				{
					normalizer = new ShortNormalizer();
				}
				else if (t == typeof(ushort))
				{
					normalizer = new UShortNormalizer();
				}
				else if (t == typeof(int))
				{
					normalizer = new IntNormalizer();
				}
				else if (t == typeof(uint))
				{
					normalizer = new UIntNormalizer();
				}
				else if (t == typeof(float))
				{
					normalizer = new FloatNormalizer();
				}
				else if (t == typeof(double))
				{
					normalizer = new DoubleNormalizer();
				}
				else if (t == typeof(long))
				{
					normalizer = new LongNormalizer();
				}
				else if (t == typeof(ulong))
				{
					normalizer = new ULongNormalizer();
				}
			}
			else if (t.IsValueType)
			{
				normalizer = new BinaryOrderedUdtNormalizer(t, false);
			}
			if (normalizer == null)
			{
				throw new Exception(Res.GetString("Sql_CanotCreateNormalizer", new object[]
				{
					t.FullName
				}));
			}
			normalizer.m_skipNormalize = false;
			return normalizer;
		}

		// Token: 0x06002217 RID: 8727
		internal abstract void Normalize(FieldInfo fi, object recvr, Stream s);

		// Token: 0x06002218 RID: 8728
		internal abstract void DeNormalize(FieldInfo fi, object recvr, Stream s);

		// Token: 0x06002219 RID: 8729 RVA: 0x0028ADD8 File Offset: 0x0028A1D8
		protected void FlipAllBits(byte[] b)
		{
			for (int i = 0; i < b.Length; i++)
			{
				b[i] = ~b[i];
			}
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0028AE08 File Offset: 0x0028A208
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		protected object GetValue(FieldInfo fi, object obj)
		{
			return fi.GetValue(obj);
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x0028AE28 File Offset: 0x0028A228
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		protected void SetValue(FieldInfo fi, object recvr, object value)
		{
			fi.SetValue(recvr, value);
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600221C RID: 8732
		internal abstract int Size { get; }

		// Token: 0x0400165A RID: 5722
		protected bool m_skipNormalize;
	}
}
