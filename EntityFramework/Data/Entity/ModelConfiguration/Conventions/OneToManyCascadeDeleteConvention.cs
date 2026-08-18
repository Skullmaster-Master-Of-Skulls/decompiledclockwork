using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020007FE RID: 2046
	public class OneToManyCascadeDeleteConvention : IConceptualModelConvention<AssociationType>, IConvention
	{
		// Token: 0x06005C6A RID: 23658 RVA: 0x0018F0A4 File Offset: 0x0018D2A4
		public virtual void Apply(AssociationType item, DbModel model)
		{
			Check.NotNull<AssociationType>(item, "item");
			Check.NotNull<DbModel>(model, "model");
			if (item.IsSelfReferencing())
			{
				return;
			}
			NavigationPropertyConfiguration navigationPropertyConfiguration = item.GetConfiguration() as NavigationPropertyConfiguration;
			if (navigationPropertyConfiguration != null && navigationPropertyConfiguration.DeleteAction != null)
			{
				return;
			}
			AssociationEndMember associationEndMember = null;
			if (item.IsRequiredToMany())
			{
				associationEndMember = item.SourceEnd;
			}
			else if (item.IsManyToRequired())
			{
				associationEndMember = item.TargetEnd;
			}
			if (associationEndMember != null)
			{
				associationEndMember.DeleteBehavior = OperationAction.Cascade;
			}
		}
	}
}
