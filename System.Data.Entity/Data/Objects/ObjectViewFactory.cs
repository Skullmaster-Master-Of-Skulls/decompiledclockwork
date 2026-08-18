using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Data.Objects
{
	// Token: 0x0200015A RID: 346
	internal static class ObjectViewFactory
	{
		// Token: 0x0600198E RID: 6542 RVA: 0x0005951C File Offset: 0x0005771C
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal static IBindingList CreateViewForQuery<TElement>(TypeUsage elementEdmTypeUsage, IEnumerable<TElement> queryResults, ObjectContext objectContext, bool forceReadOnly, EntitySet singleEntitySet)
		{
			EntityUtil.CheckArgumentNull<IEnumerable<TElement>>(queryResults, "queryResults");
			EntityUtil.CheckArgumentNull<ObjectContext>(objectContext, "objectContext");
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
				Type type2 = ObjectViewFactory.genericObjectViewQueryResultDataType.MakeGenericType(new Type[]
				{
					type
				});
				ConstructorInfo constructor = type2.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
				{
					typeof(IEnumerable),
					typeof(ObjectContext),
					typeof(bool),
					typeof(EntitySet)
				}, null);
				object viewData3 = constructor.Invoke(new object[]
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

		// Token: 0x0600198F RID: 6543 RVA: 0x00059684 File Offset: 0x00057884
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
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
				Type type2 = ObjectViewFactory.genericObjectViewEntityCollectionDataType.MakeGenericType(new Type[]
				{
					type,
					typeof(TElement)
				});
				ConstructorInfo constructor = type2.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
				{
					typeof(EntityCollection<TElement>)
				}, null);
				object viewData2 = constructor.Invoke(new object[]
				{
					entityCollection
				});
				result = ObjectViewFactory.CreateObjectView(type, type2, viewData2, entityCollection);
			}
			return result;
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0005978C File Offset: 0x0005798C
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private static IBindingList CreateObjectView(Type clrElementType, Type objectViewDataType, object viewData, object eventDataSource)
		{
			Type type2 = ObjectViewFactory.genericObjectViewType.MakeGenericType(new Type[]
			{
				clrElementType
			});
			Type[] array = objectViewDataType.FindInterfaces((Type type, object unusedFilter) => type.Name == ObjectViewFactory.genericObjectViewDataInterfaceType.Name, null);
			ConstructorInfo constructor = type2.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[]
			{
				array[0],
				typeof(object)
			}, null);
			return (IBindingList)constructor.Invoke(new object[]
			{
				viewData,
				eventDataSource
			});
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00059814 File Offset: 0x00057A14
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
				objectContext.EnsureMetadata();
				result = objectContext.Perspective.MetadataWorkspace.GetOSpaceTypeUsage(typeUsage);
			}
			return result;
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x00059864 File Offset: 0x00057A64
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

		// Token: 0x04000AED RID: 2797
		private static readonly Type genericObjectViewType = typeof(ObjectView<>);

		// Token: 0x04000AEE RID: 2798
		private static readonly Type genericObjectViewDataInterfaceType = typeof(IObjectViewData<>);

		// Token: 0x04000AEF RID: 2799
		private static readonly Type genericObjectViewQueryResultDataType = typeof(ObjectViewQueryResultData<>);

		// Token: 0x04000AF0 RID: 2800
		private static readonly Type genericObjectViewEntityCollectionDataType = typeof(ObjectViewEntityCollectionData<, >);
	}
}
