using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005B4 RID: 1460
	internal static class ObjectViewFactory
	{
		// Token: 0x06003A79 RID: 14969 RVA: 0x0011615C File Offset: 0x0011435C
		internal static IBindingList CreateViewForQuery<TElement>(TypeUsage elementEdmTypeUsage, IEnumerable<TElement> queryResults, ObjectContext objectContext, bool forceReadOnly, EntitySet singleEntitySet)
		{
			TypeUsage ospaceTypeUsage = ObjectViewFactory.GetOSpaceTypeUsage(elementEdmTypeUsage, objectContext);
			Type type;
			if (ospaceTypeUsage == null)
			{
				type = typeof(TElement);
			}
			type = ObjectViewFactory.GetClrType<TElement>(ospaceTypeUsage.EdmType);
			object objectStateManager = objectContext.ObjectStateManager;
			IBindingList result;
			if (type == typeof(TElement))
			{
				ObjectViewQueryResultData<TElement> viewData = new ObjectViewQueryResultData<TElement>(queryResults, objectContext, forceReadOnly, singleEntitySet);
				result = new ObjectView<TElement>(viewData, objectStateManager);
			}
			else if (type == null)
			{
				ObjectViewQueryResultData<DbDataRecord> viewData2 = new ObjectViewQueryResultData<DbDataRecord>(queryResults, objectContext, true, null);
				result = new DataRecordObjectView(viewData2, objectStateManager, (RowType)ospaceTypeUsage.EdmType, typeof(TElement));
			}
			else
			{
				if (!typeof(TElement).IsAssignableFrom(type))
				{
					throw EntityUtil.ValueInvalidCast(type, typeof(TElement));
				}
				Type type2 = ObjectViewFactory._genericObjectViewQueryResultDataType.MakeGenericType(new Type[]
				{
					type
				});
				ConstructorInfo declaredConstructor = type2.GetDeclaredConstructor(new Type[]
				{
					typeof(IEnumerable),
					typeof(ObjectContext),
					typeof(bool),
					typeof(EntitySet)
				});
				object viewData3 = declaredConstructor.Invoke(new object[]
				{
					queryResults,
					objectContext,
					forceReadOnly,
					singleEntitySet
				});
				result = ObjectViewFactory.CreateObjectView(type, type2, viewData3, objectStateManager);
			}
			return result;
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x001162BC File Offset: 0x001144BC
		internal static IBindingList CreateViewForEntityCollection<TElement>(EntityType entityType, EntityCollection<TElement> entityCollection) where TElement : class
		{
			TypeUsage typeUsage = (entityType == null) ? null : TypeUsage.Create(entityType);
			TypeUsage ospaceTypeUsage = ObjectViewFactory.GetOSpaceTypeUsage(typeUsage, entityCollection.ObjectContext);
			Type type;
			if (ospaceTypeUsage == null)
			{
				type = typeof(TElement);
			}
			else
			{
				type = ObjectViewFactory.GetClrType<TElement>(ospaceTypeUsage.EdmType);
				if (type == null)
				{
					type = typeof(TElement);
				}
			}
			IBindingList result;
			if (type == typeof(TElement))
			{
				ObjectViewEntityCollectionData<TElement, TElement> viewData = new ObjectViewEntityCollectionData<TElement, TElement>(entityCollection);
				result = new ObjectView<TElement>(viewData, entityCollection);
			}
			else
			{
				if (!typeof(TElement).IsAssignableFrom(type))
				{
					throw EntityUtil.ValueInvalidCast(type, typeof(TElement));
				}
				Type type2 = ObjectViewFactory._genericObjectViewEntityCollectionDataType.MakeGenericType(new Type[]
				{
					type,
					typeof(TElement)
				});
				ConstructorInfo declaredConstructor = type2.GetDeclaredConstructor(new Type[]
				{
					typeof(EntityCollection<TElement>)
				});
				object viewData2 = declaredConstructor.Invoke(new object[]
				{
					entityCollection
				});
				result = ObjectViewFactory.CreateObjectView(type, type2, viewData2, entityCollection);
			}
			return result;
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x001163E8 File Offset: 0x001145E8
		private static IBindingList CreateObjectView(Type clrElementType, Type objectViewDataType, object viewData, object eventDataSource)
		{
			Type type2 = ObjectViewFactory._genericObjectViewType.MakeGenericType(new Type[]
			{
				clrElementType
			});
			Type[] array = objectViewDataType.FindInterfaces((Type type, object unusedFilter) => type.Name == ObjectViewFactory._genericObjectViewDataInterfaceType.Name, null);
			ConstructorInfo declaredConstructor = type2.GetDeclaredConstructor(new Type[]
			{
				array[0],
				typeof(object)
			});
			return (IBindingList)declaredConstructor.Invoke(new object[]
			{
				viewData,
				eventDataSource
			});
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x00116478 File Offset: 0x00114678
		private static TypeUsage GetOSpaceTypeUsage(TypeUsage typeUsage, ObjectContext objectContext)
		{
			TypeUsage result;
			if (typeUsage == null || typeUsage.EdmType == null)
			{
				result = null;
			}
			else if (typeUsage.EdmType.DataSpace == DataSpace.OSpace)
			{
				result = typeUsage;
			}
			else if (objectContext == null)
			{
				result = null;
			}
			else
			{
				result = objectContext.Perspective.MetadataWorkspace.GetOSpaceTypeUsage(typeUsage);
			}
			return result;
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x001164C0 File Offset: 0x001146C0
		private static Type GetClrType<TElement>(EdmType ospaceEdmType)
		{
			Type type;
			if (ospaceEdmType.BuiltInTypeKind == BuiltInTypeKind.RowType)
			{
				RowType rowType = (RowType)ospaceEdmType;
				if (rowType.InitializerMetadata != null && rowType.InitializerMetadata.ClrType != null)
				{
					type = rowType.InitializerMetadata.ClrType;
				}
				else
				{
					Type typeFromHandle = typeof(TElement);
					if (typeof(IDataRecord).IsAssignableFrom(typeFromHandle) || typeFromHandle == typeof(object))
					{
						type = null;
					}
					else
					{
						type = typeof(TElement);
					}
				}
			}
			else
			{
				type = ospaceEdmType.ClrType;
				if (type == null)
				{
					type = typeof(TElement);
				}
			}
			return type;
		}

		// Token: 0x04001627 RID: 5671
		private static readonly Type _genericObjectViewType = typeof(ObjectView<>);

		// Token: 0x04001628 RID: 5672
		private static readonly Type _genericObjectViewDataInterfaceType = typeof(IObjectViewData<>);

		// Token: 0x04001629 RID: 5673
		private static readonly Type _genericObjectViewQueryResultDataType = typeof(ObjectViewQueryResultData<>);

		// Token: 0x0400162A RID: 5674
		private static readonly Type _genericObjectViewEntityCollectionDataType = typeof(ObjectViewEntityCollectionData<, >);
	}
}
