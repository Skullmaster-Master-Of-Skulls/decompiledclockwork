using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x020001C2 RID: 450
	public class AttributeToColumnAnnotationConvention<TAttribute, TAnnotation> : Convention where TAttribute : Attribute
	{
		// Token: 0x06000F27 RID: 3879 RVA: 0x00040D0C File Offset: 0x0003EF0C
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public AttributeToColumnAnnotationConvention(string annotationName, Func<PropertyInfo, IList<TAttribute>, TAnnotation> annotationFactory)
		{
			AttributeToColumnAnnotationConvention<TAttribute, TAnnotation>.<>c__DisplayClass4 CS$<>8__locals1 = new AttributeToColumnAnnotationConvention<TAttribute, TAnnotation>.<>c__DisplayClass4();
			CS$<>8__locals1.annotationName = annotationName;
			CS$<>8__locals1.annotationFactory = annotationFactory;
			base..ctor();
			Check.NotEmpty(CS$<>8__locals1.annotationName, "annotationName");
			Check.NotNull<Func<PropertyInfo, IList<TAttribute>, TAnnotation>>(CS$<>8__locals1.annotationFactory, "annotationFactory");
			AttributeProvider attributeProvider = DbConfiguration.DependencyResolver.GetService<AttributeProvider>();
			base.Properties().Having<List<TAttribute>>((PropertyInfo pi) => attributeProvider.GetAttributes(pi).OfType<TAttribute>().ToList<TAttribute>()).Configure(delegate(ConventionPrimitivePropertyConfiguration c, List<TAttribute> a)
			{
				if (a.Any<TAttribute>())
				{
					c.HasColumnAnnotation(CS$<>8__locals1.annotationName, CS$<>8__locals1.annotationFactory(c.ClrPropertyInfo, a));
				}
			});
		}
	}
}
