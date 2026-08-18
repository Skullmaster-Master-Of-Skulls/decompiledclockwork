using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoMapper
{
	// Token: 0x02000037 RID: 55
	public class ResolutionContext : IEquatable<ResolutionContext>
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00005641 File Offset: 0x00003841
		public MappingOperationOptions Options { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00005649 File Offset: 0x00003849
		public TypeMap TypeMap { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00005651 File Offset: 0x00003851
		public PropertyMap PropertyMap { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00005659 File Offset: 0x00003859
		public Type InitialSourceType { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00005661 File Offset: 0x00003861
		public Type InitialDestinationType { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00005669 File Offset: 0x00003869
		public Type SourceType { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00005671 File Offset: 0x00003871
		public Type DestinationType { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00005679 File Offset: 0x00003879
		public int? ArrayIndex { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00005681 File Offset: 0x00003881
		public object SourceValue { get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00005689 File Offset: 0x00003889
		public object DestinationValue { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00005691 File Offset: 0x00003891
		public ResolutionContext Parent { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00005699 File Offset: 0x00003899
		public Dictionary<ResolutionContext, object> InstanceCache { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000056A1 File Offset: 0x000038A1
		public IMappingEngine Engine { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000056A9 File Offset: 0x000038A9
		public IConfigurationProvider ConfigurationProvider
		{
			get
			{
				return this.Engine.ConfigurationProvider;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000056B6 File Offset: 0x000038B6
		public TypePair Types { get; }

		// Token: 0x0600023E RID: 574 RVA: 0x000056BE File Offset: 0x000038BE
		private ResolutionContext()
		{
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000056C8 File Offset: 0x000038C8
		private ResolutionContext(ResolutionContext context, object sourceValue, object destinationValue, Type sourceType, Type destinationType = null, TypeMap typeMap = null)
		{
			if (context != ResolutionContext.Empty)
			{
				if (context == null)
				{
					throw new ArgumentNullException("context");
				}
				this.Parent = context;
				this.ArrayIndex = context.ArrayIndex;
				this.PropertyMap = context.PropertyMap;
				this.DestinationType = context.DestinationType;
				this.InstanceCache = context.InstanceCache;
				this.Options = context.Options;
				this.Engine = context.Engine;
			}
			this.SourceValue = sourceValue;
			this.DestinationValue = destinationValue;
			this.InitialSourceType = sourceType;
			this.InitialDestinationType = destinationType;
			this.TypeMap = typeMap;
			if (typeMap != null)
			{
				this.SourceType = typeMap.SourceType;
				this.DestinationType = typeMap.DestinationType;
			}
			else
			{
				this.SourceType = sourceType;
				if (destinationType != null)
				{
					this.DestinationType = destinationType;
				}
			}
			this.Types = new TypePair(this.SourceType, this.DestinationType);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000057B7 File Offset: 0x000039B7
		public ResolutionContext(TypeMap typeMap, object source, Type sourceType, Type destinationType, MappingOperationOptions options, IMappingEngine engine) : this(typeMap, source, null, sourceType, destinationType, options, engine)
		{
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000057C9 File Offset: 0x000039C9
		public ResolutionContext(TypeMap typeMap, object source, object destination, Type sourceType, Type destinationType, MappingOperationOptions options, IMappingEngine engine) : this(ResolutionContext.Empty, source, destination, sourceType, destinationType, typeMap)
		{
			this.InstanceCache = new Dictionary<ResolutionContext, object>();
			this.Options = options;
			this.Engine = engine;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000057F8 File Offset: 0x000039F8
		private ResolutionContext(ResolutionContext context, object sourceValue, Type sourceType) : this(context, sourceValue, context.DestinationValue, sourceType, null, null)
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000580B File Offset: 0x00003A0B
		private ResolutionContext(ResolutionContext context, TypeMap memberTypeMap, object sourceValue, object destinationValue, Type sourceType, Type destinationType) : this(context, sourceValue, destinationValue, sourceType, destinationType, memberTypeMap)
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000581C File Offset: 0x00003A1C
		private ResolutionContext(ResolutionContext context, object sourceValue, object destinationValue, TypeMap memberTypeMap, PropertyMap propertyMap) : this(context, sourceValue, destinationValue, null, null, memberTypeMap)
		{
			if (memberTypeMap == null)
			{
				throw new ArgumentNullException("memberTypeMap");
			}
			this.PropertyMap = propertyMap;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00005844 File Offset: 0x00003A44
		private ResolutionContext(ResolutionContext context, object sourceValue, object destinationValue, Type sourceType, PropertyMap propertyMap) : this(context, sourceValue, destinationValue, sourceType, (propertyMap.DestinationProperty.MemberType == typeof(object)) ? sourceType : propertyMap.DestinationProperty.MemberType, null)
		{
			this.PropertyMap = propertyMap;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00005892 File Offset: 0x00003A92
		private ResolutionContext(ResolutionContext context, object sourceValue, TypeMap typeMap, Type sourceType, Type destinationType, int arrayIndex) : this(context, sourceValue, null, sourceType, destinationType, typeMap)
		{
			this.ArrayIndex = new int?(arrayIndex);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000058B0 File Offset: 0x00003AB0
		public string MemberName
		{
			get
			{
				if (this.PropertyMap == null)
				{
					return string.Empty;
				}
				if (this.ArrayIndex != null)
				{
					return this.PropertyMap.DestinationProperty.Name + this.ArrayIndex.Value;
				}
				return this.PropertyMap.DestinationProperty.Name;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00005914 File Offset: 0x00003B14
		public bool IsSourceValueNull
		{
			get
			{
				return object.Equals(null, this.SourceValue);
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00005922 File Offset: 0x00003B22
		public ResolutionContext CreateValueContext(object sourceValue, Type sourceType)
		{
			return new ResolutionContext(this, sourceValue, sourceType);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000592C File Offset: 0x00003B2C
		public ResolutionContext CreateTypeContext(TypeMap memberTypeMap, object sourceValue, object destinationValue, Type sourceType, Type destinationType)
		{
			return new ResolutionContext(this, memberTypeMap, sourceValue, destinationValue, sourceType, destinationType);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000593B File Offset: 0x00003B3B
		public ResolutionContext CreatePropertyMapContext(PropertyMap propertyMap)
		{
			return new ResolutionContext(this, this.SourceValue, this.DestinationValue, this.SourceType, propertyMap);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00005956 File Offset: 0x00003B56
		public ResolutionContext CreateMemberContext(TypeMap memberTypeMap, object memberValue, object destinationValue, Type sourceMemberType, PropertyMap propertyMap)
		{
			if (memberTypeMap == null)
			{
				return new ResolutionContext(this, memberValue, destinationValue, sourceMemberType, propertyMap);
			}
			return new ResolutionContext(this, memberValue, destinationValue, memberTypeMap, propertyMap);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00005973 File Offset: 0x00003B73
		public ResolutionContext CreateElementContext(TypeMap elementTypeMap, object item, Type sourceElementType, Type destinationElementType, int arrayIndex)
		{
			return new ResolutionContext(this, item, elementTypeMap, sourceElementType, destinationElementType, arrayIndex);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00005982 File Offset: 0x00003B82
		public override string ToString()
		{
			return string.Format("Trying to map {0} to {1}.", this.SourceType.Name, this.DestinationType.Name);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000059A4 File Offset: 0x00003BA4
		public TypeMap GetContextTypeMap()
		{
			TypeMap typeMap = this.TypeMap;
			ResolutionContext parent = this.Parent;
			while (typeMap == null && parent != null)
			{
				typeMap = parent.TypeMap;
				parent = parent.Parent;
			}
			return typeMap;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000059D8 File Offset: 0x00003BD8
		public PropertyMap GetContextPropertyMap()
		{
			PropertyMap propertyMap = this.PropertyMap;
			ResolutionContext parent = this.Parent;
			while (propertyMap == null && parent != null)
			{
				propertyMap = parent.PropertyMap;
				parent = parent.Parent;
			}
			return propertyMap;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00005A0C File Offset: 0x00003C0C
		public bool Equals(ResolutionContext other)
		{
			return other != null && (this == other || (object.Equals(other.TypeMap, this.TypeMap) && object.Equals(other.SourceType, this.SourceType) && object.Equals(other.DestinationType, this.DestinationType) && object.Equals(other.SourceValue, this.SourceValue)));
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00005A70 File Offset: 0x00003C70
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != typeof(ResolutionContext)) && this.Equals((ResolutionContext)obj)));
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public override int GetHashCode()
		{
			return ((((this.TypeMap != null) ? this.TypeMap.GetHashCode() : 0) * 397 ^ ((this.SourceType != null) ? this.SourceType.GetHashCode() : 0)) * 397 ^ ((this.DestinationType != null) ? this.DestinationType.GetHashCode() : 0)) * 397 ^ ((this.SourceValue != null) ? this.SourceValue.GetHashCode() : 0);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00005B2A File Offset: 0x00003D2A
		public ResolutionContext[] GetContexts()
		{
			return this.GetContextsCore().Reverse<ResolutionContext>().Distinct<ResolutionContext>().ToArray<ResolutionContext>();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00005B41 File Offset: 0x00003D41
		protected IEnumerable<ResolutionContext> GetContextsCore()
		{
			ResolutionContext context = this;
			while (context.Parent != null)
			{
				yield return context;
				context = context.Parent;
			}
			yield return context;
			yield break;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00005B51 File Offset: 0x00003D51
		public static ResolutionContext New<TSource>(TSource sourceValue, IMappingEngine mappingEngine)
		{
			return new ResolutionContext(null, sourceValue, typeof(TSource), null, new MappingOperationOptions(), mappingEngine);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00005B70 File Offset: 0x00003D70
		internal void BeforeMap(object destination)
		{
			if (this.Parent == null)
			{
				this.Options.BeforeMapAction(this.SourceValue, destination);
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00005B91 File Offset: 0x00003D91
		internal void AfterMap(object destination)
		{
			if (this.Parent == null)
			{
				this.Options.AfterMapAction(this.SourceValue, destination);
			}
		}

		// Token: 0x04000056 RID: 86
		private static readonly ResolutionContext Empty = new ResolutionContext();
	}
}
