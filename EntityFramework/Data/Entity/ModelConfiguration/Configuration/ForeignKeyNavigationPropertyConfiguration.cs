using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020007CC RID: 1996
	public class ForeignKeyNavigationPropertyConfiguration : CascadableNavigationPropertyConfiguration
	{
		// Token: 0x06005A9B RID: 23195 RVA: 0x00186722 File Offset: 0x00184922
		internal ForeignKeyNavigationPropertyConfiguration(NavigationPropertyConfiguration navigationPropertyConfiguration) : base(navigationPropertyConfiguration)
		{
		}

		// Token: 0x06005A9C RID: 23196 RVA: 0x0018672C File Offset: 0x0018492C
		public CascadableNavigationPropertyConfiguration Map(Action<ForeignKeyAssociationMappingConfiguration> configurationAction)
		{
			Check.NotNull<Action<ForeignKeyAssociationMappingConfiguration>>(configurationAction, "configurationAction");
			base.NavigationPropertyConfiguration.Constraint = IndependentConstraintConfiguration.Instance;
			ForeignKeyAssociationMappingConfiguration foreignKeyAssociationMappingConfiguration = new ForeignKeyAssociationMappingConfiguration();
			configurationAction(foreignKeyAssociationMappingConfiguration);
			base.NavigationPropertyConfiguration.AssociationMappingConfiguration = foreignKeyAssociationMappingConfiguration;
			return this;
		}

		// Token: 0x06005A9D RID: 23197 RVA: 0x0018676F File Offset: 0x0018496F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005A9E RID: 23198 RVA: 0x00186777 File Offset: 0x00184977
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005A9F RID: 23199 RVA: 0x00186780 File Offset: 0x00184980
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005AA0 RID: 23200 RVA: 0x00186788 File Offset: 0x00184988
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
