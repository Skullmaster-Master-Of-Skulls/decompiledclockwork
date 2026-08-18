using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper.Internal;
using AutoMapper.Mappers;

namespace AutoMapper
{
	// Token: 0x02000035 RID: 53
	public abstract class Profile : IProfileExpression, IProfileConfiguration
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x00004CBB File Offset: 0x00002EBB
		protected Profile(string profileName) : this()
		{
			this.ProfileName = profileName;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00004CCC File Offset: 0x00002ECC
		protected Profile()
		{
			this.ProfileName = base.GetType().FullName;
			this.AllowNullDestinationValues = true;
			this.ConstructorMappingEnabled = true;
			this.IncludeSourceExtensionMethods(typeof(Enumerable).Assembly());
			this.ShouldMapProperty = ((PropertyInfo p) => p.IsPublic());
			this.ShouldMapField = ((FieldInfo f) => f.IsPublic);
			ConditionalObjectMapper conditionalObjectMapper = new ConditionalObjectMapper(this.ProfileName);
			conditionalObjectMapper.Conventions.Add((TypePair tp) => true);
			this._mapMissingTypes = conditionalObjectMapper;
			this._globalIgnore = new List<string>();
			this._memberConfigurations.Add(new MemberConfiguration().AddMember<NameSplitMember>(null).AddName<PrePostfixName>(delegate(PrePostfixName _)
			{
				_.AddStrings((PrePostfixName p) => p.Prefixes, new string[]
				{
					"Get"
				});
			}));
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00004DFE File Offset: 0x00002FFE
		public virtual string ProfileName { get; }

		// Token: 0x060001DC RID: 476 RVA: 0x00004E06 File Offset: 0x00003006
		public void DisableConstructorMapping()
		{
			this.ConstructorMappingEnabled = false;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00004E0F File Offset: 0x0000300F
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00004E17 File Offset: 0x00003017
		public bool AllowNullDestinationValues { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00004E20 File Offset: 0x00003020
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00004E28 File Offset: 0x00003028
		public bool AllowNullCollections { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00004E31 File Offset: 0x00003031
		public IEnumerable<string> GlobalIgnores
		{
			get
			{
				return this._globalIgnore;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00004E3C File Offset: 0x0000303C
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00004E74 File Offset: 0x00003074
		public INamingConvention SourceMemberNamingConvention
		{
			get
			{
				INamingConvention convention = null;
				this.DefaultMemberConfig.AddMember<NameSplitMember>(delegate(NameSplitMember _)
				{
					convention = _.SourceMemberNamingConvention;
				});
				return convention;
			}
			set
			{
				this.DefaultMemberConfig.AddMember<NameSplitMember>(delegate(NameSplitMember _)
				{
					_.SourceMemberNamingConvention = value;
				});
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00004EA8 File Offset: 0x000030A8
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00004EE0 File Offset: 0x000030E0
		public INamingConvention DestinationMemberNamingConvention
		{
			get
			{
				INamingConvention convention = null;
				this.DefaultMemberConfig.AddMember<NameSplitMember>(delegate(NameSplitMember _)
				{
					convention = _.DestinationMemberNamingConvention;
				});
				return convention;
			}
			set
			{
				this.DefaultMemberConfig.AddMember<NameSplitMember>(delegate(NameSplitMember _)
				{
					_.DestinationMemberNamingConvention = value;
				});
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00004F12 File Offset: 0x00003112
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00004F1A File Offset: 0x0000311A
		public bool CreateMissingTypeMaps
		{
			get
			{
				return this._createMissingTypeMaps;
			}
			set
			{
				this._createMissingTypeMaps = value;
				if (value)
				{
					this._typeConfigurations.Add(this._mapMissingTypes);
					return;
				}
				this._typeConfigurations.Remove(this._mapMissingTypes);
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00004F4A File Offset: 0x0000314A
		public void ForAllMaps(Action<TypeMap, IMappingExpression> configuration)
		{
			this._configurator.ForAllMaps(this.ProfileName, configuration);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00004F5E File Offset: 0x0000315E
		public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>()
		{
			return this.CreateMap<TSource, TDestination>(MemberList.Destination);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00004F67 File Offset: 0x00003167
		public IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>(MemberList memberList)
		{
			return this._configurator.CreateMap<TSource, TDestination>(this.ProfileName, memberList);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00004F7B File Offset: 0x0000317B
		public IMappingExpression CreateMap(Type sourceType, Type destinationType)
		{
			return this.CreateMap(sourceType, destinationType, MemberList.Destination);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00004F86 File Offset: 0x00003186
		public IMappingExpression CreateMap(Type sourceType, Type destinationType, MemberList memberList)
		{
			return this._configurator.CreateMap(sourceType, destinationType, memberList, this.ProfileName);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00004F9C File Offset: 0x0000319C
		public void ClearPrefixes()
		{
			this.DefaultMemberConfig.AddName<PrePostfixName>(delegate(PrePostfixName _)
			{
				_.Prefixes.Clear();
			});
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00004FCC File Offset: 0x000031CC
		public void RecognizeAlias(string original, string alias)
		{
			this.DefaultMemberConfig.AddName<ReplaceName>(delegate(ReplaceName _)
			{
				_.AddReplace(original, alias);
			});
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00005008 File Offset: 0x00003208
		public void ReplaceMemberName(string original, string newValue)
		{
			this.DefaultMemberConfig.AddName<ReplaceName>(delegate(ReplaceName _)
			{
				_.AddReplace(original, newValue);
			});
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00005044 File Offset: 0x00003244
		public void RecognizePrefixes(params string[] prefixes)
		{
			this.DefaultMemberConfig.AddName<PrePostfixName>(delegate(PrePostfixName _)
			{
				_.AddStrings((PrePostfixName p) => p.Prefixes, prefixes);
			});
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00005078 File Offset: 0x00003278
		public void RecognizePostfixes(params string[] postfixes)
		{
			this.DefaultMemberConfig.AddName<PrePostfixName>(delegate(PrePostfixName _)
			{
				_.AddStrings((PrePostfixName p) => p.Postfixes, postfixes);
			});
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000050AC File Offset: 0x000032AC
		public void RecognizeDestinationPrefixes(params string[] prefixes)
		{
			this.DefaultMemberConfig.AddName<PrePostfixName>(delegate(PrePostfixName _)
			{
				_.AddStrings((PrePostfixName p) => p.DestinationPrefixes, prefixes);
			});
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000050E0 File Offset: 0x000032E0
		public void RecognizeDestinationPostfixes(params string[] postfixes)
		{
			this.DefaultMemberConfig.AddName<PrePostfixName>(delegate(PrePostfixName _)
			{
				_.AddStrings((PrePostfixName p) => p.DestinationPostfixes, postfixes);
			});
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00005112 File Offset: 0x00003312
		public void AddGlobalIgnore(string propertyNameStartingWith)
		{
			this._globalIgnore.Add(propertyNameStartingWith);
		}

		// Token: 0x060001F5 RID: 501
		protected abstract void Configure();

		// Token: 0x060001F6 RID: 502 RVA: 0x00005120 File Offset: 0x00003320
		public void Initialize(IConfiguration configurator)
		{
			this._configurator = configurator;
			this.Configure();
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000512F File Offset: 0x0000332F
		public IMemberConfiguration DefaultMemberConfig
		{
			get
			{
				return this._memberConfigurations.First<IMemberConfiguration>();
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000513C File Offset: 0x0000333C
		public IEnumerable<IMemberConfiguration> MemberConfigurations
		{
			get
			{
				return this._memberConfigurations;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00005144 File Offset: 0x00003344
		public IMemberConfiguration AddMemberConfiguration()
		{
			MemberConfiguration memberConfiguration = new MemberConfiguration();
			this._memberConfigurations.Add(memberConfiguration);
			return memberConfiguration;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00005164 File Offset: 0x00003364
		public IEnumerable<IConditionalObjectMapper> TypeConfigurations
		{
			get
			{
				return this._typeConfigurations;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000516C File Offset: 0x0000336C
		public IConditionalObjectMapper AddConditionalObjectMapper()
		{
			ConditionalObjectMapper conditionalObjectMapper = new ConditionalObjectMapper(this.ProfileName);
			this._typeConfigurations.Add(conditionalObjectMapper);
			return conditionalObjectMapper;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00005192 File Offset: 0x00003392
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000519A File Offset: 0x0000339A
		public bool ConstructorMappingEnabled { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001FE RID: 510 RVA: 0x000051A3 File Offset: 0x000033A3
		public IEnumerable<MethodInfo> SourceExtensionMethods
		{
			get
			{
				return this._sourceExtensionMethods;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000051AB File Offset: 0x000033AB
		// (set) Token: 0x06000200 RID: 512 RVA: 0x000051B3 File Offset: 0x000033B3
		public Func<PropertyInfo, bool> ShouldMapProperty { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000201 RID: 513 RVA: 0x000051BC File Offset: 0x000033BC
		// (set) Token: 0x06000202 RID: 514 RVA: 0x000051C4 File Offset: 0x000033C4
		public Func<FieldInfo, bool> ShouldMapField { get; set; }

		// Token: 0x06000203 RID: 515 RVA: 0x000051D0 File Offset: 0x000033D0
		public void IncludeSourceExtensionMethods(Assembly assembly)
		{
			this._sourceExtensionMethods.AddRange(from method in (from type in assembly.ExportedTypes
			where type.IsSealed() && !type.IsGenericType() && !type.IsNested
			select type).SelectMany((Type type) => from mi in type.GetDeclaredMethods()
			where mi.IsStatic
			select mi)
			where method.IsDefined(typeof(ExtensionAttribute), false)
			where method.GetParameters().Length == 1
			select method);
		}

		// Token: 0x04000039 RID: 57
		private IConfiguration _configurator;

		// Token: 0x0400003A RID: 58
		private readonly IConditionalObjectMapper _mapMissingTypes;

		// Token: 0x0400003B RID: 59
		private readonly List<string> _globalIgnore;

		// Token: 0x0400003F RID: 63
		private readonly List<MethodInfo> _sourceExtensionMethods = new List<MethodInfo>();

		// Token: 0x04000040 RID: 64
		private readonly IList<IMemberConfiguration> _memberConfigurations = new List<IMemberConfiguration>();

		// Token: 0x04000041 RID: 65
		private readonly IList<IConditionalObjectMapper> _typeConfigurations = new List<IConditionalObjectMapper>();

		// Token: 0x04000042 RID: 66
		private bool _createMissingTypeMaps;
	}
}
