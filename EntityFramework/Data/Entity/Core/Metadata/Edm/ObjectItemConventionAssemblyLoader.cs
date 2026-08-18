using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200051D RID: 1309
	internal class ObjectItemConventionAssemblyLoader : ObjectItemAssemblyLoader
	{
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x000EC364 File Offset: 0x000EA564
		public new virtual MutableAssemblyCacheEntry CacheEntry
		{
			get
			{
				return (MutableAssemblyCacheEntry)base.CacheEntry;
			}
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000EC371 File Offset: 0x000EA571
		internal ObjectItemConventionAssemblyLoader(Assembly assembly, ObjectItemLoadingSessionData sessionData) : base(assembly, new MutableAssemblyCacheEntry(), sessionData)
		{
			base.SessionData.RegisterForLevel1PostSessionProcessing(this);
			this._factory = new ObjectItemConventionAssemblyLoader.ConventionOSpaceTypeFactory(this);
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x000EC3A4 File Offset: 0x000EA5A4
		protected override void LoadTypesFromAssembly()
		{
			foreach (Type type in base.SourceAssembly.GetAccessibleTypes())
			{
				EdmType edmType;
				if (this.TryGetCSpaceTypeMatch(type, out edmType))
				{
					if (type.IsValueType() && !type.IsEnum())
					{
						base.SessionData.LoadMessageLogger.LogLoadMessage(Strings.Validator_OSpace_Convention_Struct(edmType.FullName, type.FullName), edmType);
					}
					else
					{
						EdmType edmType2 = this._factory.TryCreateType(type, edmType);
						if (edmType2 != null)
						{
							this.CacheEntry.TypesInAssembly.Add(edmType2);
							if (!base.SessionData.CspaceToOspace.ContainsKey(edmType))
							{
								base.SessionData.CspaceToOspace.Add(edmType, edmType2);
							}
							else
							{
								EdmType edmType3 = base.SessionData.CspaceToOspace[edmType];
								base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_Convention_AmbiguousClrType(edmType.Name, edmType3.ClrType.FullName, type.FullName)));
							}
						}
					}
				}
			}
			if (base.SessionData.TypesInLoading.Count == 0)
			{
				base.SessionData.ObjectItemAssemblyLoaderFactory = null;
			}
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x000EC4EC File Offset: 0x000EA6EC
		protected override void AddToAssembliesLoaded()
		{
			base.SessionData.AssembliesLoaded.Add(base.SourceAssembly, this.CacheEntry);
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000EC50C File Offset: 0x000EA70C
		private bool TryGetCSpaceTypeMatch(Type type, out EdmType cspaceType)
		{
			KeyValuePair<EdmType, int> keyValuePair;
			if (base.SessionData.ConventionCSpaceTypeNames.TryGetValue(type.Name, out keyValuePair))
			{
				if (keyValuePair.Value == 1)
				{
					cspaceType = keyValuePair.Key;
					return true;
				}
				base.SessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_Convention_MultipleTypesWithSameName(type.Name)));
			}
			cspaceType = null;
			return false;
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000EC56C File Offset: 0x000EA76C
		internal override void OnLevel1SessionProcessing()
		{
			this.CreateRelationships();
			foreach (Action action in this._referenceResolutions)
			{
				action();
			}
			base.OnLevel1SessionProcessing();
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000EC5CC File Offset: 0x000EA7CC
		internal virtual void TrackClosure(Type type)
		{
			if (base.SourceAssembly != type.Assembly() && !this.CacheEntry.ClosureAssemblies.Contains(type.Assembly()) && (!type.IsGenericType() || (!EntityUtil.IsAnICollection(type) && !(type.GetGenericTypeDefinition() == typeof(EntityReference<>)) && !(type.GetGenericTypeDefinition() == typeof(Nullable<>)))))
			{
				this.CacheEntry.ClosureAssemblies.Add(type.Assembly());
			}
			if (type.IsGenericType())
			{
				foreach (Type type2 in type.GetGenericArguments())
				{
					this.TrackClosure(type2);
				}
			}
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000EC680 File Offset: 0x000EA880
		private void CreateRelationships()
		{
			if (base.SessionData.ConventionBasedRelationshipsAreLoaded)
			{
				return;
			}
			base.SessionData.ConventionBasedRelationshipsAreLoaded = true;
			this._factory.CreateRelationships(base.SessionData.EdmItemCollection);
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000EC6B2 File Offset: 0x000EA8B2
		internal static bool SessionContainsConventionParameters(ObjectItemLoadingSessionData sessionData)
		{
			return sessionData.EdmItemCollection != null;
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000EC6C0 File Offset: 0x000EA8C0
		internal static ObjectItemAssemblyLoader Create(Assembly assembly, ObjectItemLoadingSessionData sessionData)
		{
			if (!ObjectItemAttributeAssemblyLoader.IsSchemaAttributePresent(assembly))
			{
				return new ObjectItemConventionAssemblyLoader(assembly, sessionData);
			}
			sessionData.EdmItemErrors.Add(new EdmItemError(Strings.Validator_OSpace_Convention_AttributeAssemblyReferenced(assembly.FullName)));
			return new ObjectItemNoOpAssemblyLoader(assembly, sessionData);
		}

		// Token: 0x0400129B RID: 4763
		private readonly List<Action> _referenceResolutions = new List<Action>();

		// Token: 0x0400129C RID: 4764
		private readonly ObjectItemConventionAssemblyLoader.ConventionOSpaceTypeFactory _factory;

		// Token: 0x0200051E RID: 1310
		internal class ConventionOSpaceTypeFactory : OSpaceTypeFactory
		{
			// Token: 0x06003158 RID: 12632 RVA: 0x000EC6F4 File Offset: 0x000EA8F4
			public ConventionOSpaceTypeFactory(ObjectItemConventionAssemblyLoader loader)
			{
				this._loader = loader;
			}

			// Token: 0x17000759 RID: 1881
			// (get) Token: 0x06003159 RID: 12633 RVA: 0x000EC703 File Offset: 0x000EA903
			public override List<Action> ReferenceResolutions
			{
				get
				{
					return this._loader._referenceResolutions;
				}
			}

			// Token: 0x0600315A RID: 12634 RVA: 0x000EC710 File Offset: 0x000EA910
			public override void LogLoadMessage(string message, EdmType relatedType)
			{
				this._loader.SessionData.LoadMessageLogger.LogLoadMessage(message, relatedType);
			}

			// Token: 0x0600315B RID: 12635 RVA: 0x000EC72C File Offset: 0x000EA92C
			public override void LogError(string errorMessage, EdmType relatedType)
			{
				string message = this._loader.SessionData.LoadMessageLogger.CreateErrorMessageWithTypeSpecificLoadLogs(errorMessage, relatedType);
				this._loader.SessionData.EdmItemErrors.Add(new EdmItemError(message));
			}

			// Token: 0x0600315C RID: 12636 RVA: 0x000EC76C File Offset: 0x000EA96C
			public override void TrackClosure(Type type)
			{
				this._loader.TrackClosure(type);
			}

			// Token: 0x1700075A RID: 1882
			// (get) Token: 0x0600315D RID: 12637 RVA: 0x000EC77A File Offset: 0x000EA97A
			public override Dictionary<EdmType, EdmType> CspaceToOspace
			{
				get
				{
					return this._loader.SessionData.CspaceToOspace;
				}
			}

			// Token: 0x1700075B RID: 1883
			// (get) Token: 0x0600315E RID: 12638 RVA: 0x000EC78C File Offset: 0x000EA98C
			public override Dictionary<string, EdmType> LoadedTypes
			{
				get
				{
					return this._loader.SessionData.TypesInLoading;
				}
			}

			// Token: 0x0600315F RID: 12639 RVA: 0x000EC79E File Offset: 0x000EA99E
			public override void AddToTypesInAssembly(EdmType type)
			{
				this._loader.CacheEntry.TypesInAssembly.Add(type);
			}

			// Token: 0x0400129D RID: 4765
			private readonly ObjectItemConventionAssemblyLoader _loader;
		}
	}
}
