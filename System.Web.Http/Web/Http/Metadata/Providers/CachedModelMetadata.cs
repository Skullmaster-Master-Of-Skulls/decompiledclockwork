using System;

namespace System.Web.Http.Metadata.Providers
{
	// Token: 0x02000136 RID: 310
	public abstract class CachedModelMetadata<TPrototypeCache> : ModelMetadata
	{
		// Token: 0x060007BC RID: 1980 RVA: 0x00019E74 File Offset: 0x00018074
		protected CachedModelMetadata(CachedModelMetadata<TPrototypeCache> prototype, Func<object> modelAccessor) : base(prototype.Provider, prototype.ContainerType, modelAccessor, prototype.ModelType, prototype.PropertyName, prototype.CacheKey)
		{
			this.PrototypeCache = prototype.PrototypeCache;
			this._isComplexType = prototype.IsComplexType;
			this._isComplexTypeComputed = true;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00019EC5 File Offset: 0x000180C5
		protected CachedModelMetadata(DataAnnotationsModelMetadataProvider provider, Type containerType, Type modelType, string propertyName, TPrototypeCache prototypeCache) : base(provider, containerType, null, modelType, propertyName)
		{
			this.PrototypeCache = prototypeCache;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00019EDB File Offset: 0x000180DB
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x00019EFE File Offset: 0x000180FE
		public sealed override bool ConvertEmptyStringToNull
		{
			get
			{
				if (!this._convertEmptyStringToNullComputed)
				{
					this._convertEmptyStringToNull = this.ComputeConvertEmptyStringToNull();
					this._convertEmptyStringToNullComputed = true;
				}
				return this._convertEmptyStringToNull;
			}
			set
			{
				this._convertEmptyStringToNull = value;
				this._convertEmptyStringToNullComputed = true;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00019F0E File Offset: 0x0001810E
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x00019F31 File Offset: 0x00018131
		public sealed override string Description
		{
			get
			{
				if (!this._descriptionComputed)
				{
					this._description = this.ComputeDescription();
					this._descriptionComputed = true;
				}
				return this._description;
			}
			set
			{
				this._description = value;
				this._descriptionComputed = true;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00019F41 File Offset: 0x00018141
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x00019F64 File Offset: 0x00018164
		public sealed override bool IsReadOnly
		{
			get
			{
				if (!this._isReadOnlyComputed)
				{
					this._isReadOnly = this.ComputeIsReadOnly();
					this._isReadOnlyComputed = true;
				}
				return this._isReadOnly;
			}
			set
			{
				this._isReadOnly = value;
				this._isReadOnlyComputed = true;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00019F74 File Offset: 0x00018174
		public sealed override bool IsComplexType
		{
			get
			{
				if (!this._isComplexTypeComputed)
				{
					this._isComplexType = this.ComputeIsComplexType();
					this._isComplexTypeComputed = true;
				}
				return this._isComplexType;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00019F97 File Offset: 0x00018197
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x00019F9F File Offset: 0x0001819F
		protected TPrototypeCache PrototypeCache { get; set; }

		// Token: 0x060007C7 RID: 1991 RVA: 0x00019FA8 File Offset: 0x000181A8
		protected virtual bool ComputeConvertEmptyStringToNull()
		{
			return base.ConvertEmptyStringToNull;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00019FB0 File Offset: 0x000181B0
		protected virtual string ComputeDescription()
		{
			return base.Description;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00019FB8 File Offset: 0x000181B8
		protected virtual bool ComputeIsReadOnly()
		{
			return base.IsReadOnly;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00019FC0 File Offset: 0x000181C0
		protected virtual bool ComputeIsComplexType()
		{
			return base.IsComplexType;
		}

		// Token: 0x0400023F RID: 575
		private bool _convertEmptyStringToNull;

		// Token: 0x04000240 RID: 576
		private string _description;

		// Token: 0x04000241 RID: 577
		private bool _isReadOnly;

		// Token: 0x04000242 RID: 578
		private bool _isComplexType;

		// Token: 0x04000243 RID: 579
		private bool _convertEmptyStringToNullComputed;

		// Token: 0x04000244 RID: 580
		private bool _descriptionComputed;

		// Token: 0x04000245 RID: 581
		private bool _isReadOnlyComputed;

		// Token: 0x04000246 RID: 582
		private bool _isComplexTypeComputed;
	}
}
