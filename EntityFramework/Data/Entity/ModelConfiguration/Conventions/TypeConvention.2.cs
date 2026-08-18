using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001CC RID: 460
	internal class TypeConvention<T> : TypeConventionBase where T : class
	{
		// Token: 0x06000F49 RID: 3913 RVA: 0x000412F3 File Offset: 0x0003F4F3
		public TypeConvention(IEnumerable<Func<Type, bool>> predicates, Action<ConventionTypeConfiguration<T>> entityConfigurationAction) : base(predicates.Prepend(TypeConvention<T>._ofTypePredicate))
		{
			this._entityConfigurationAction = entityConfigurationAction;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x0004130D File Offset: 0x0003F50D
		internal Action<ConventionTypeConfiguration<T>> EntityConfigurationAction
		{
			get
			{
				return this._entityConfigurationAction;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x00041315 File Offset: 0x0003F515
		internal static Func<Type, bool> OfTypePredicate
		{
			get
			{
				return TypeConvention<T>._ofTypePredicate;
			}
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0004131C File Offset: 0x0003F51C
		protected override void ApplyCore(Type memberInfo, ModelConfiguration modelConfiguration)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration<T>(memberInfo, modelConfiguration));
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00041330 File Offset: 0x0003F530
		protected override void ApplyCore(Type memberInfo, Func<EntityTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration<T>(memberInfo, configuration, modelConfiguration));
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00041345 File Offset: 0x0003F545
		protected override void ApplyCore(Type memberInfo, Func<ComplexTypeConfiguration> configuration, ModelConfiguration modelConfiguration)
		{
			this._entityConfigurationAction(new ConventionTypeConfiguration<T>(memberInfo, configuration, modelConfiguration));
		}

		// Token: 0x04000422 RID: 1058
		private static readonly Func<Type, bool> _ofTypePredicate = (Type t) => typeof(T).IsAssignableFrom(t);

		// Token: 0x04000423 RID: 1059
		private readonly Action<ConventionTypeConfiguration<T>> _entityConfigurationAction;
	}
}
