using System;
using System.Globalization;

namespace System.Collections.Specialized
{
	// Token: 0x020003AF RID: 943
	[Serializable]
	internal class CompatibleComparer : IEqualityComparer
	{
		// Token: 0x06002358 RID: 9048 RVA: 0x000A7A15 File Offset: 0x000A5C15
		internal CompatibleComparer(IComparer comparer, IHashCodeProvider hashCodeProvider)
		{
			this._comparer = comparer;
			this._hcp = hashCodeProvider;
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000A7A2C File Offset: 0x000A5C2C
		public bool Equals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			try
			{
				if (this._comparer != null)
				{
					return this._comparer.Compare(a, b) == 0;
				}
				IComparable comparable = a as IComparable;
				if (comparable != null)
				{
					return comparable.CompareTo(b) == 0;
				}
			}
			catch (ArgumentException)
			{
				return false;
			}
			return a.Equals(b);
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000A7A9C File Offset: 0x000A5C9C
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._hcp != null)
			{
				return this._hcp.GetHashCode(obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x000A7AC7 File Offset: 0x000A5CC7
		public IComparer Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x0600235C RID: 9052 RVA: 0x000A7ACF File Offset: 0x000A5CCF
		public IHashCodeProvider HashCodeProvider
		{
			get
			{
				return this._hcp;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x0600235D RID: 9053 RVA: 0x000A7AD7 File Offset: 0x000A5CD7
		public static IComparer DefaultComparer
		{
			get
			{
				if (CompatibleComparer.defaultComparer == null)
				{
					CompatibleComparer.defaultComparer = new CaseInsensitiveComparer(CultureInfo.InvariantCulture);
				}
				return CompatibleComparer.defaultComparer;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x000A7AFA File Offset: 0x000A5CFA
		public static IHashCodeProvider DefaultHashCodeProvider
		{
			get
			{
				if (CompatibleComparer.defaultHashProvider == null)
				{
					CompatibleComparer.defaultHashProvider = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture);
				}
				return CompatibleComparer.defaultHashProvider;
			}
		}

		// Token: 0x04001FD9 RID: 8153
		private IComparer _comparer;

		// Token: 0x04001FDA RID: 8154
		private static volatile IComparer defaultComparer;

		// Token: 0x04001FDB RID: 8155
		private IHashCodeProvider _hcp;

		// Token: 0x04001FDC RID: 8156
		private static volatile IHashCodeProvider defaultHashProvider;
	}
}
