using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001C3 RID: 451
	public class AttributeToTableAnnotationConvention<TAttribute, TAnnotation> : Convention where TAttribute : Attribute
	{
		// Token: 0x06000F28 RID: 3880 RVA: 0x00040DF8 File Offset: 0x0003EFF8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public AttributeToTableAnnotationConvention(string annotationName, Func<Type, IList<TAttribute>, TAnnotation> annotationFactory)
		{
			AttributeToTableAnnotationConvention<TAttribute, TAnnotation>.<>c__DisplayClass3 CS$<>8__locals1 = new AttributeToTableAnnotationConvention<TAttribute, TAnnotation>.<>c__DisplayClass3();
			CS$<>8__locals1.annotationName = annotationName;
			CS$<>8__locals1.annotationFactory = annotationFactory;
			base..ctor();
			Check.NotEmpty(CS$<>8__locals1.annotationName, "annotationName");
			Check.NotNull<Func<Type, IList<TAttribute>, TAnnotation>>(CS$<>8__locals1.annotationFactory, "annotationFactory");
			AttributeProvider attributeProvider = DbConfiguration.DependencyResolver.GetService<AttributeProvider>();
			base.Types().Having<List<TAttribute>>((Type t) => attributeProvider.GetAttributes(t).OfType<TAttribute>().ToList<TAttribute>()).Configure(delegate(ConventionTypeConfiguration c, List<TAttribute> a)
			{
				if (a.Any<TAttribute>())
				{
					c.HasTableAnnotation(CS$<>8__locals1.annotationName, CS$<>8__locals1.annotationFactory(c.ClrType, a));
				}
			});
		}
	}
}
