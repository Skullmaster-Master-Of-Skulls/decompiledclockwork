using System;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.DataAnnotations.Schema
{
	// Token: 0x020000D8 RID: 216
	[SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	[SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
	public class IndexAttribute : Attribute
	{
		// Token: 0x06000568 RID: 1384 RVA: 0x000246A7 File Offset: 0x000228A7
		public IndexAttribute()
		{
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000246B6 File Offset: 0x000228B6
		public IndexAttribute(string name)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000246D8 File Offset: 0x000228D8
		public IndexAttribute(string name, int order)
		{
			Check.NotEmpty(name, "name");
			if (order < 0)
			{
				throw new ArgumentOutOfRangeException("order");
			}
			this._name = name;
			this._order = order;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00024710 File Offset: 0x00022910
		private IndexAttribute(string name, int order, bool? isClustered, bool? isUnique)
		{
			this._name = name;
			this._order = order;
			this._isClustered = isClustered;
			this._isUnique = isUnique;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0002473C File Offset: 0x0002293C
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x00024744 File Offset: 0x00022944
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			internal set
			{
				this._name = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0002474D File Offset: 0x0002294D
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x00024755 File Offset: 0x00022955
		public virtual int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._order = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0002476D File Offset: 0x0002296D
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x00024789 File Offset: 0x00022989
		public virtual bool IsClustered
		{
			get
			{
				return this._isClustered != null && this._isClustered.Value;
			}
			set
			{
				this._isClustered = new bool?(value);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00024797 File Offset: 0x00022997
		public virtual bool IsClusteredConfigured
		{
			get
			{
				return this._isClustered != null;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x000247A4 File Offset: 0x000229A4
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x000247C0 File Offset: 0x000229C0
		public virtual bool IsUnique
		{
			get
			{
				return this._isUnique != null && this._isUnique.Value;
			}
			set
			{
				this._isUnique = new bool?(value);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x000247CE File Offset: 0x000229CE
		public virtual bool IsUniqueConfigured
		{
			get
			{
				return this._isUnique != null;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x000247DB File Offset: 0x000229DB
		public override object TypeId
		{
			get
			{
				return RuntimeHelpers.GetHashCode(this);
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000247E8 File Offset: 0x000229E8
		protected virtual bool Equals(IndexAttribute other)
		{
			return this._name == other._name && this._order == other._order && this._isClustered.Equals(other._isClustered) && this._isUnique.Equals(other._isUnique);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00024852 File Offset: 0x00022A52
		public override string ToString()
		{
			return IndexAnnotationSerializer.SerializeIndexAttribute(this);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0002485A File Offset: 0x00022A5A
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != base.GetType()) && this.Equals((IndexAttribute)obj)));
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00024894 File Offset: 0x00022A94
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			num = (num * 397 ^ ((this._name != null) ? this._name.GetHashCode() : 0));
			num = (num * 397 ^ this._order);
			num = (num * 397 ^ this._isClustered.GetHashCode());
			return num * 397 ^ this._isUnique.GetHashCode();
		}

		// Token: 0x040001B4 RID: 436
		private string _name;

		// Token: 0x040001B5 RID: 437
		private int _order = -1;

		// Token: 0x040001B6 RID: 438
		private bool? _isClustered;

		// Token: 0x040001B7 RID: 439
		private bool? _isUnique;
	}
}
