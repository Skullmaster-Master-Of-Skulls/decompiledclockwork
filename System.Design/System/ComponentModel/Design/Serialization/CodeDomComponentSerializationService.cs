using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000577 RID: 1399
	public sealed class CodeDomComponentSerializationService : ComponentSerializationService
	{
		// Token: 0x0600317F RID: 12671 RVA: 0x00117DE4 File Offset: 0x00116DE4
		public CodeDomComponentSerializationService() : this(null)
		{
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x00117DED File Offset: 0x00116DED
		public CodeDomComponentSerializationService(IServiceProvider provider)
		{
			this._provider = provider;
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x00117DFC File Offset: 0x00116DFC
		public override SerializationStore CreateStore()
		{
			return new CodeDomComponentSerializationService.CodeDomSerializationStore(this._provider);
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x00117E09 File Offset: 0x00116E09
		public override SerializationStore LoadStore(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return CodeDomComponentSerializationService.CodeDomSerializationStore.Load(stream);
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x00117E20 File Offset: 0x00116E20
		public override void Serialize(SerializationStore store, object value)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			CodeDomComponentSerializationService.CodeDomSerializationStore codeDomSerializationStore = store as CodeDomComponentSerializationService.CodeDomSerializationStore;
			if (codeDomSerializationStore == null)
			{
				throw new InvalidOperationException(SR.GetString("CodeDomComponentSerializationServiceUnknownStore"));
			}
			codeDomSerializationStore.AddObject(value, false);
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x00117E6C File Offset: 0x00116E6C
		public override void SerializeAbsolute(SerializationStore store, object value)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			CodeDomComponentSerializationService.CodeDomSerializationStore codeDomSerializationStore = store as CodeDomComponentSerializationService.CodeDomSerializationStore;
			if (codeDomSerializationStore == null)
			{
				throw new InvalidOperationException(SR.GetString("CodeDomComponentSerializationServiceUnknownStore"));
			}
			codeDomSerializationStore.AddObject(value, true);
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x00117EB8 File Offset: 0x00116EB8
		public override void SerializeMember(SerializationStore store, object owningObject, MemberDescriptor member)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			if (owningObject == null)
			{
				throw new ArgumentNullException("owningObject");
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			CodeDomComponentSerializationService.CodeDomSerializationStore codeDomSerializationStore = store as CodeDomComponentSerializationService.CodeDomSerializationStore;
			if (codeDomSerializationStore == null)
			{
				throw new InvalidOperationException(SR.GetString("CodeDomComponentSerializationServiceUnknownStore"));
			}
			codeDomSerializationStore.AddMember(owningObject, member, false);
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x00117F14 File Offset: 0x00116F14
		public override void SerializeMemberAbsolute(SerializationStore store, object owningObject, MemberDescriptor member)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			if (owningObject == null)
			{
				throw new ArgumentNullException("owningObject");
			}
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			CodeDomComponentSerializationService.CodeDomSerializationStore codeDomSerializationStore = store as CodeDomComponentSerializationService.CodeDomSerializationStore;
			if (codeDomSerializationStore == null)
			{
				throw new InvalidOperationException(SR.GetString("CodeDomComponentSerializationServiceUnknownStore"));
			}
			codeDomSerializationStore.AddMember(owningObject, member, true);
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x00117F70 File Offset: 0x00116F70
		public override ICollection Deserialize(SerializationStore store)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			CodeDomComponentSerializationService.CodeDomSerializationStore codeDomSerializationStore = store as CodeDomComponentSerializationService.CodeDomSerializationStore;
			if (codeDomSerializationStore == null)
			{
				throw new InvalidOperationException(SR.GetString("CodeDomComponentSerializationServiceUnknownStore"));
			}
			return codeDomSerializationStore.Deserialize(this._provider);
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x00117FB4 File Offset: 0x00116FB4
		public override ICollection Deserialize(SerializationStore store, IContainer container)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			CodeDomComponentSerializationService.CodeDomSerializationStore codeDomSerializationStore = store as CodeDomComponentSerializationService.CodeDomSerializationStore;
			if (codeDomSerializationStore == null)
			{
				throw new InvalidOperationException(SR.GetString("CodeDomComponentSerializationServiceUnknownStore"));
			}
			return codeDomSerializationStore.Deserialize(this._provider, container);
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x00118004 File Offset: 0x00117004
		public override void DeserializeTo(SerializationStore store, IContainer container, bool validateRecycledTypes, bool applyDefaults)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			CodeDomComponentSerializationService.CodeDomSerializationStore codeDomSerializationStore = store as CodeDomComponentSerializationService.CodeDomSerializationStore;
			if (codeDomSerializationStore == null)
			{
				throw new InvalidOperationException(SR.GetString("CodeDomComponentSerializationServiceUnknownStore"));
			}
			codeDomSerializationStore.DeserializeTo(this._provider, container, validateRecycledTypes, applyDefaults);
		}

		// Token: 0x04002113 RID: 8467
		private IServiceProvider _provider;

		// Token: 0x02000578 RID: 1400
		[Serializable]
		private sealed class CodeDomSerializationStore : SerializationStore, ISerializable
		{
			// Token: 0x0600318A RID: 12682 RVA: 0x00118057 File Offset: 0x00117057
			internal CodeDomSerializationStore(IServiceProvider provider)
			{
				this._provider = provider;
				this._objects = new Hashtable();
				this._objectNames = new ArrayList();
				this._shimObjectNames = new List<string>();
			}

			// Token: 0x0600318B RID: 12683 RVA: 0x00118088 File Offset: 0x00117088
			private CodeDomSerializationStore(SerializationInfo info, StreamingContext context)
			{
				this._objectState = (Hashtable)info.GetValue("State", typeof(Hashtable));
				this._objectNames = (ArrayList)info.GetValue("Names", typeof(ArrayList));
				this._assemblies = (AssemblyName[])info.GetValue("Assemblies", typeof(AssemblyName[]));
				this._shimObjectNames = (List<string>)info.GetValue("Shim", typeof(List<string>));
				Hashtable hashtable = (Hashtable)info.GetValue("Resources", typeof(Hashtable));
				if (hashtable != null)
				{
					this._resources = new CodeDomComponentSerializationService.CodeDomSerializationStore.LocalResourceManager(hashtable);
				}
			}

			// Token: 0x17000940 RID: 2368
			// (get) Token: 0x0600318C RID: 12684 RVA: 0x00118145 File Offset: 0x00117145
			private AssemblyName[] AssemblyNames
			{
				get
				{
					return this._assemblies;
				}
			}

			// Token: 0x17000941 RID: 2369
			// (get) Token: 0x0600318D RID: 12685 RVA: 0x00118150 File Offset: 0x00117150
			public override ICollection Errors
			{
				get
				{
					if (this._errors == null)
					{
						this._errors = new object[0];
					}
					object[] array = new object[this._errors.Count];
					this._errors.CopyTo(array, 0);
					return array;
				}
			}

			// Token: 0x17000942 RID: 2370
			// (get) Token: 0x0600318E RID: 12686 RVA: 0x00118190 File Offset: 0x00117190
			private CodeDomComponentSerializationService.CodeDomSerializationStore.LocalResourceManager Resources
			{
				get
				{
					if (this._resources == null)
					{
						this._resources = new CodeDomComponentSerializationService.CodeDomSerializationStore.LocalResourceManager();
					}
					return this._resources;
				}
			}

			// Token: 0x0600318F RID: 12687 RVA: 0x001181AC File Offset: 0x001171AC
			internal void AddMember(object value, MemberDescriptor member, bool absolute)
			{
				if (this._objectState != null)
				{
					throw new InvalidOperationException(SR.GetString("CodeDomComponentSerializationServiceClosedStore"));
				}
				CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectData objectData = (CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectData)this._objects[value];
				if (objectData == null)
				{
					objectData = new CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectData();
					objectData.Name = this.GetObjectName(value);
					objectData.Value = value;
					this._objects[value] = objectData;
					this._objectNames.Add(objectData.Name);
				}
				objectData.Members.Add(new CodeDomComponentSerializationService.CodeDomSerializationStore.MemberData(member, absolute));
			}

			// Token: 0x06003190 RID: 12688 RVA: 0x00118234 File Offset: 0x00117234
			internal void AddObject(object value, bool absolute)
			{
				if (this._objectState != null)
				{
					throw new InvalidOperationException(SR.GetString("CodeDomComponentSerializationServiceClosedStore"));
				}
				CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectData objectData = (CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectData)this._objects[value];
				if (objectData == null)
				{
					objectData = new CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectData();
					objectData.Name = this.GetObjectName(value);
					objectData.Value = value;
					this._objects[value] = objectData;
					this._objectNames.Add(objectData.Name);
				}
				objectData.EntireObject = true;
				objectData.Absolute = absolute;
			}

			// Token: 0x06003191 RID: 12689 RVA: 0x001182B8 File Offset: 0x001172B8
			public override void Close()
			{
				if (this._objectState == null)
				{
					Hashtable objectState = new Hashtable(this._objects.Count);
					DesignerSerializationManager designerSerializationManager = new DesignerSerializationManager(new CodeDomComponentSerializationService.CodeDomSerializationStore.LocalServices(this, this._provider));
					DesignerSerializationManager designerSerializationManager2 = this._provider.GetService(typeof(IDesignerSerializationManager)) as DesignerSerializationManager;
					if (designerSerializationManager2 != null)
					{
						foreach (object obj in designerSerializationManager2.SerializationProviders)
						{
							IDesignerSerializationProvider provider = (IDesignerSerializationProvider)obj;
							((IDesignerSerializationManager)designerSerializationManager).AddSerializationProvider(provider);
						}
					}
					using (designerSerializationManager.CreateSession())
					{
						foreach (object obj2 in this._objects.Values)
						{
							CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectData objectData = (CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectData)obj2;
							((IDesignerSerializationManager)designerSerializationManager).SetName(objectData.Value, objectData.Name);
						}
						CodeDomComponentSerializationService.CodeDomSerializationStore.ComponentListCodeDomSerializer.Instance.Serialize(designerSerializationManager, this._objects, objectState, this._shimObjectNames);
						this._errors = designerSerializationManager.Errors;
					}
					if (this._resources != null && this._resourceStream == null)
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						this._resourceStream = new MemoryStream();
						binaryFormatter.Serialize(this._resourceStream, this._resources.Data);
					}
					Hashtable hashtable = new Hashtable(this._objects.Count);
					foreach (object obj3 in this._objects.Keys)
					{
						Assembly assembly = obj3.GetType().Assembly;
						hashtable[assembly] = null;
					}
					this._assemblies = new AssemblyName[hashtable.Count];
					int num = 0;
					foreach (object obj4 in hashtable.Keys)
					{
						Assembly assembly2 = (Assembly)obj4;
						this._assemblies[num++] = assembly2.GetName(true);
					}
					this._objectState = objectState;
					this._objects = null;
				}
			}

			// Token: 0x06003192 RID: 12690 RVA: 0x0011853C File Offset: 0x0011753C
			internal ICollection Deserialize(IServiceProvider provider)
			{
				return this.Deserialize(provider, null, false, true, true);
			}

			// Token: 0x06003193 RID: 12691 RVA: 0x00118549 File Offset: 0x00117549
			internal ICollection Deserialize(IServiceProvider provider, IContainer container)
			{
				return this.Deserialize(provider, container, false, true, true);
			}

			// Token: 0x06003194 RID: 12692 RVA: 0x00118558 File Offset: 0x00117558
			private ICollection Deserialize(IServiceProvider provider, IContainer container, bool recycleInstances, bool validateRecycledTypes, bool applyDefaults)
			{
				CodeDomComponentSerializationService.CodeDomSerializationStore.PassThroughSerializationManager passThroughSerializationManager = new CodeDomComponentSerializationService.CodeDomSerializationStore.PassThroughSerializationManager(new CodeDomComponentSerializationService.CodeDomSerializationStore.LocalDesignerSerializationManager(this, new CodeDomComponentSerializationService.CodeDomSerializationStore.LocalServices(this, provider)));
				if (container != null)
				{
					passThroughSerializationManager.Manager.Container = container;
				}
				DesignerSerializationManager designerSerializationManager = provider.GetService(typeof(IDesignerSerializationManager)) as DesignerSerializationManager;
				if (designerSerializationManager != null)
				{
					foreach (object obj in designerSerializationManager.SerializationProviders)
					{
						IDesignerSerializationProvider provider2 = (IDesignerSerializationProvider)obj;
						((IDesignerSerializationManager)passThroughSerializationManager.Manager).AddSerializationProvider(provider2);
					}
				}
				passThroughSerializationManager.Manager.RecycleInstances = recycleInstances;
				passThroughSerializationManager.Manager.PreserveNames = recycleInstances;
				passThroughSerializationManager.Manager.ValidateRecycledTypes = validateRecycledTypes;
				ArrayList arrayList = null;
				if (this._resourceStream != null)
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					this._resourceStream.Seek(0L, SeekOrigin.Begin);
					Hashtable data = binaryFormatter.Deserialize(this._resourceStream) as Hashtable;
					this._resources = new CodeDomComponentSerializationService.CodeDomSerializationStore.LocalResourceManager(data);
				}
				if (!recycleInstances)
				{
					arrayList = new ArrayList(this._objectNames.Count);
				}
				using (passThroughSerializationManager.Manager.CreateSession())
				{
					if (this._shimObjectNames.Count > 0)
					{
						List<string> shimObjectNames = this._shimObjectNames;
						IDesignerSerializationManager designerSerializationManager2 = passThroughSerializationManager;
						if (designerSerializationManager2 != null && container != null)
						{
							foreach (string name in shimObjectNames)
							{
								object obj2 = container.Components[name];
								if (obj2 != null && designerSerializationManager2.GetInstance(name) == null)
								{
									designerSerializationManager2.SetName(obj2, name);
								}
							}
						}
					}
					CodeDomComponentSerializationService.CodeDomSerializationStore.ComponentListCodeDomSerializer.Instance.Deserialize(passThroughSerializationManager, this._objectState, this._objectNames, applyDefaults);
					if (!recycleInstances)
					{
						foreach (object obj3 in this._objectNames)
						{
							string name2 = (string)obj3;
							object instance = ((IDesignerSerializationManager)passThroughSerializationManager.Manager).GetInstance(name2);
							if (instance != null)
							{
								arrayList.Add(instance);
							}
						}
					}
					this._errors = passThroughSerializationManager.Manager.Errors;
				}
				return arrayList;
			}

			// Token: 0x06003195 RID: 12693 RVA: 0x001187B0 File Offset: 0x001177B0
			internal void DeserializeTo(IServiceProvider provider, IContainer container, bool validateRecycledTypes, bool applyDefaults)
			{
				this.Deserialize(provider, container, true, validateRecycledTypes, applyDefaults);
			}

			// Token: 0x06003196 RID: 12694 RVA: 0x001187C0 File Offset: 0x001177C0
			private string GetObjectName(object value)
			{
				IComponent component = value as IComponent;
				if (component != null)
				{
					ISite site = component.Site;
					if (site != null)
					{
						INestedSite nestedSite = site as INestedSite;
						if (nestedSite != null && !string.IsNullOrEmpty(nestedSite.FullName))
						{
							return nestedSite.FullName;
						}
						if (!string.IsNullOrEmpty(site.Name))
						{
							return site.Name;
						}
					}
				}
				string text = Guid.NewGuid().ToString();
				text = text.Replace("-", "_");
				return string.Format(CultureInfo.CurrentCulture, "object_{0}", new object[]
				{
					text
				});
			}

			// Token: 0x06003197 RID: 12695 RVA: 0x0011885C File Offset: 0x0011785C
			internal static CodeDomComponentSerializationService.CodeDomSerializationStore Load(Stream stream)
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				return (CodeDomComponentSerializationService.CodeDomSerializationStore)binaryFormatter.Deserialize(stream);
			}

			// Token: 0x06003198 RID: 12696 RVA: 0x0011887C File Offset: 0x0011787C
			public override void Save(Stream stream)
			{
				this.Close();
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(stream, this);
			}

			// Token: 0x06003199 RID: 12697 RVA: 0x0011889D File Offset: 0x0011789D
			[Conditional("DEBUG")]
			internal static void Trace(string message, params object[] args)
			{
			}

			// Token: 0x0600319A RID: 12698 RVA: 0x001188A0 File Offset: 0x001178A0
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				Hashtable value = null;
				if (this._resources != null)
				{
					value = this._resources.Data;
				}
				info.AddValue("State", this._objectState);
				info.AddValue("Names", this._objectNames);
				info.AddValue("Assemblies", this._assemblies);
				info.AddValue("Resources", value);
				info.AddValue("Shim", this._shimObjectNames);
			}

			// Token: 0x04002114 RID: 8468
			private const string _stateKey = "State";

			// Token: 0x04002115 RID: 8469
			private const string _nameKey = "Names";

			// Token: 0x04002116 RID: 8470
			private const string _assembliesKey = "Assemblies";

			// Token: 0x04002117 RID: 8471
			private const string _resourcesKey = "Resources";

			// Token: 0x04002118 RID: 8472
			private const string _shimKey = "Shim";

			// Token: 0x04002119 RID: 8473
			private const int _stateCode = 0;

			// Token: 0x0400211A RID: 8474
			private const int _stateCtx = 1;

			// Token: 0x0400211B RID: 8475
			private const int _stateProperties = 2;

			// Token: 0x0400211C RID: 8476
			private const int _stateResources = 3;

			// Token: 0x0400211D RID: 8477
			private const int _stateEvents = 4;

			// Token: 0x0400211E RID: 8478
			private const int _stateModifier = 5;

			// Token: 0x0400211F RID: 8479
			private MemoryStream _resourceStream;

			// Token: 0x04002120 RID: 8480
			private Hashtable _objects;

			// Token: 0x04002121 RID: 8481
			private IServiceProvider _provider;

			// Token: 0x04002122 RID: 8482
			private ArrayList _objectNames;

			// Token: 0x04002123 RID: 8483
			private Hashtable _objectState;

			// Token: 0x04002124 RID: 8484
			private CodeDomComponentSerializationService.CodeDomSerializationStore.LocalResourceManager _resources;

			// Token: 0x04002125 RID: 8485
			private AssemblyName[] _assemblies;

			// Token: 0x04002126 RID: 8486
			private List<string> _shimObjectNames;

			// Token: 0x04002127 RID: 8487
			private ICollection _errors;

			// Token: 0x02000579 RID: 1401
			private class ComponentListCodeDomSerializer : CodeDomSerializer
			{
				// Token: 0x0600319B RID: 12699 RVA: 0x00118913 File Offset: 0x00117913
				public override object Deserialize(IDesignerSerializationManager manager, object state)
				{
					throw new NotSupportedException();
				}

				// Token: 0x0600319C RID: 12700 RVA: 0x0011891C File Offset: 0x0011791C
				private void PopulateCompleteStatements(object data, string name, CodeStatementCollection completeStatements)
				{
					CodeStatementCollection value;
					if ((value = (data as CodeStatementCollection)) != null)
					{
						completeStatements.AddRange(value);
						return;
					}
					CodeStatement value2;
					if ((value2 = (data as CodeStatement)) != null)
					{
						completeStatements.Add(value2);
						return;
					}
					CodeExpression value3;
					if ((value3 = (data as CodeExpression)) != null)
					{
						ArrayList arrayList = null;
						if (this._expressions.ContainsKey(name))
						{
							arrayList = this._expressions[name];
						}
						if (arrayList == null)
						{
							arrayList = new ArrayList();
							this._expressions[name] = arrayList;
						}
						arrayList.Add(value3);
					}
				}

				// Token: 0x0600319D RID: 12701 RVA: 0x00118994 File Offset: 0x00117994
				internal void Deserialize(IDesignerSerializationManager manager, IDictionary objectState, IList objectNames, bool applyDefaults)
				{
					CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
					this._expressions = new Dictionary<string, ArrayList>();
					this.applyDefaults = applyDefaults;
					foreach (object obj in objectNames)
					{
						string text = (string)obj;
						object[] array = (object[])objectState[text];
						if (array != null)
						{
							if (array[0] != null)
							{
								this.PopulateCompleteStatements(array[0], text, codeStatementCollection);
							}
							if (array[1] != null)
							{
								this.PopulateCompleteStatements(array[1], text, codeStatementCollection);
							}
						}
					}
					CodeStatementCollection codeStatementCollection2 = new CodeStatementCollection();
					CodeMethodMap codeMethodMap = new CodeMethodMap(codeStatementCollection2, null);
					codeMethodMap.Add(codeStatementCollection);
					codeMethodMap.Combine();
					this._statementsTable = new Hashtable();
					CodeDomSerializerBase.FillStatementTable(manager, this._statementsTable, codeStatementCollection2);
					ArrayList arrayList = new ArrayList(objectNames);
					foreach (object obj2 in this._statementsTable.Keys)
					{
						string text2 = (string)obj2;
						if (!arrayList.Contains(text2))
						{
							arrayList.Add(text2);
						}
					}
					this._objectState = new Hashtable(objectState.Keys.Count);
					foreach (object obj3 in objectState)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
						this._objectState.Add(dictionaryEntry.Key, dictionaryEntry.Value);
					}
					ResolveNameEventHandler value = new ResolveNameEventHandler(this.OnResolveName);
					manager.ResolveName += value;
					try
					{
						foreach (object obj4 in arrayList)
						{
							string name = (string)obj4;
							this.ResolveName(manager, name, true);
						}
					}
					finally
					{
						this._objectState = null;
						manager.ResolveName -= value;
					}
				}

				// Token: 0x0600319E RID: 12702 RVA: 0x00118BCC File Offset: 0x00117BCC
				private void OnResolveName(object sender, ResolveNameEventArgs e)
				{
					if (this._nameResolveGuard.ContainsKey(e.Name))
					{
						return;
					}
					this._nameResolveGuard.Add(e.Name, true);
					try
					{
						IDesignerSerializationManager designerSerializationManager = (IDesignerSerializationManager)sender;
						if (this.ResolveName(designerSerializationManager, e.Name, false))
						{
							e.Value = designerSerializationManager.GetInstance(e.Name);
						}
					}
					finally
					{
						this._nameResolveGuard.Remove(e.Name);
					}
				}

				// Token: 0x0600319F RID: 12703 RVA: 0x00118C54 File Offset: 0x00117C54
				private void DeserializeDefaultProperties(IDesignerSerializationManager manager, string name, object state)
				{
					if (state != null && this.applyDefaults)
					{
						object instance = manager.GetInstance(name);
						if (instance != null)
						{
							PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(instance);
							string[] array = (string[])state;
							MemberRelationshipService memberRelationshipService = manager.GetService(typeof(MemberRelationshipService)) as MemberRelationshipService;
							foreach (string name2 in array)
							{
								PropertyDescriptor propertyDescriptor = properties[name2];
								if (propertyDescriptor != null && propertyDescriptor.CanResetValue(instance))
								{
									if (memberRelationshipService != null && memberRelationshipService[instance, propertyDescriptor] != MemberRelationship.Empty)
									{
										memberRelationshipService[instance, propertyDescriptor] = MemberRelationship.Empty;
									}
									propertyDescriptor.ResetValue(instance);
								}
							}
						}
					}
				}

				// Token: 0x060031A0 RID: 12704 RVA: 0x00118D08 File Offset: 0x00117D08
				private void DeserializeDesignTimeProperties(IDesignerSerializationManager manager, string name, object state)
				{
					if (state != null)
					{
						object instance = manager.GetInstance(name);
						if (instance != null)
						{
							PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(instance);
							foreach (object obj in ((IDictionary)state))
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
								PropertyDescriptor propertyDescriptor = properties[(string)dictionaryEntry.Key];
								if (propertyDescriptor != null)
								{
									propertyDescriptor.SetValue(instance, dictionaryEntry.Value);
								}
							}
						}
					}
				}

				// Token: 0x060031A1 RID: 12705 RVA: 0x00118D9C File Offset: 0x00117D9C
				private IComponent ResolveNestedName(IDesignerSerializationManager manager, string name, ref string outerComponent)
				{
					IComponent component = null;
					if (name != null && manager != null)
					{
						bool flag = true;
						int num = name.IndexOf('.', 0);
						outerComponent = name.Substring(0, num);
						component = (manager.GetInstance(outerComponent) as IComponent);
						int num2 = num;
						int num3 = name.IndexOf('.', num + 1);
						while (flag)
						{
							flag = (num3 != -1);
							string text = flag ? name.Substring(num2 + 1, num3) : name.Substring(num2 + 1);
							if (component == null || component.Site == null)
							{
								return null;
							}
							ISite site = component.Site;
							INestedContainer nestedContainer = site.GetService(typeof(INestedContainer)) as INestedContainer;
							if (nestedContainer == null || string.IsNullOrEmpty(text))
							{
								return null;
							}
							component = nestedContainer.Components[text];
							if (flag)
							{
								num2 = num3;
								num3 = name.IndexOf('.', num3 + 1);
							}
						}
					}
					return component;
				}

				// Token: 0x060031A2 RID: 12706 RVA: 0x00118E7C File Offset: 0x00117E7C
				private bool ResolveName(IDesignerSerializationManager manager, string name, bool canInvokeManager)
				{
					bool flag = false;
					CodeDomSerializerBase.OrderedCodeStatementCollection orderedCodeStatementCollection = this._statementsTable[name] as CodeDomSerializerBase.OrderedCodeStatementCollection;
					object[] array = (object[])this._objectState[name];
					if (name.IndexOf('.') > 0)
					{
						string text = null;
						IComponent component = this.ResolveNestedName(manager, name, ref text);
						if (component != null && text != null)
						{
							manager.SetName(component, name);
							this.ResolveName(manager, text, canInvokeManager);
						}
					}
					if (orderedCodeStatementCollection != null)
					{
						this._objectState[name] = null;
						this._statementsTable[name] = null;
						string text2 = null;
						foreach (object obj in orderedCodeStatementCollection)
						{
							CodeStatement codeStatement = (CodeStatement)obj;
							CodeVariableDeclarationStatement codeVariableDeclarationStatement;
							if ((codeVariableDeclarationStatement = (codeStatement as CodeVariableDeclarationStatement)) != null)
							{
								text2 = codeVariableDeclarationStatement.Type.BaseType;
								break;
							}
						}
						if (text2 != null)
						{
							Type type = manager.GetType(text2);
							if (type == null)
							{
								manager.ReportError(new CodeDomSerializerException(SR.GetString("SerializerTypeNotFound", new object[]
								{
									text2
								}), manager));
								goto IL_1D4;
							}
							if (orderedCodeStatementCollection == null || orderedCodeStatementCollection.Count <= 0)
							{
								goto IL_1D4;
							}
							CodeDomSerializer serializer = base.GetSerializer(manager, type);
							if (serializer == null)
							{
								manager.ReportError(new CodeDomSerializerException(SR.GetString("SerializerNoSerializerForComponent", new object[]
								{
									type.FullName
								}), manager));
								goto IL_1D4;
							}
							try
							{
								object obj2 = serializer.Deserialize(manager, orderedCodeStatementCollection);
								flag = (obj2 != null);
								if (flag)
								{
									this._statementsTable[name] = obj2;
								}
								goto IL_1D4;
							}
							catch (Exception errorInformation)
							{
								manager.ReportError(errorInformation);
								goto IL_1D4;
							}
						}
						foreach (object obj3 in orderedCodeStatementCollection)
						{
							CodeStatement statement = (CodeStatement)obj3;
							base.DeserializeStatement(manager, statement);
						}
						flag = true;
						IL_1D4:
						if (array != null && array[2] != null)
						{
							this.DeserializeDefaultProperties(manager, name, array[2]);
						}
						if (array != null && array[3] != null)
						{
							this.DeserializeDesignTimeProperties(manager, name, array[3]);
						}
						if (array != null && array[4] != null)
						{
							this.DeserializeEventResets(manager, name, array[4]);
						}
						if (array != null && array[5] != null)
						{
							CodeDomComponentSerializationService.CodeDomSerializationStore.ComponentListCodeDomSerializer.DeserializeModifier(manager, name, array[5]);
						}
						if (this._expressions.ContainsKey(name))
						{
							ArrayList arrayList = this._expressions[name];
							foreach (object obj4 in arrayList)
							{
								CodeExpression expression = (CodeExpression)obj4;
								base.DeserializeExpression(manager, name, expression);
							}
							this._expressions.Remove(name);
							flag = true;
						}
					}
					else
					{
						flag = (this._statementsTable[name] != null);
						if (!flag)
						{
							if (this._expressions.ContainsKey(name))
							{
								ArrayList arrayList2 = this._expressions[name];
								foreach (object obj5 in arrayList2)
								{
									CodeExpression expression2 = (CodeExpression)obj5;
									object obj6 = base.DeserializeExpression(manager, name, expression2);
									if (obj6 != null && !flag && canInvokeManager && manager.GetInstance(name) == null)
									{
										manager.SetName(obj6, name);
										flag = true;
									}
								}
							}
							if (!flag && canInvokeManager)
							{
								flag = (manager.GetInstance(name) != null);
							}
							if (flag && array != null && array[2] != null)
							{
								this.DeserializeDefaultProperties(manager, name, array[2]);
							}
							if (flag && array != null && array[3] != null)
							{
								this.DeserializeDesignTimeProperties(manager, name, array[3]);
							}
							if (flag && array != null && array[4] != null)
							{
								this.DeserializeEventResets(manager, name, array[4]);
							}
							if (flag && array != null && array[5] != null)
							{
								CodeDomComponentSerializationService.CodeDomSerializationStore.ComponentListCodeDomSerializer.DeserializeModifier(manager, name, array[5]);
							}
						}
						if (!flag && (flag || canInvokeManager))
						{
							manager.ReportError(new CodeDomSerializerException(SR.GetString("CodeDomComponentSerializationServiceDeserializationError", new object[]
							{
								name
							}), manager));
						}
					}
					return flag;
				}

				// Token: 0x060031A3 RID: 12707 RVA: 0x00119294 File Offset: 0x00118294
				private void DeserializeEventResets(IDesignerSerializationManager manager, string name, object state)
				{
					List<string> list = state as List<string>;
					if (list != null && manager != null && !string.IsNullOrEmpty(name))
					{
						IEventBindingService eventBindingService = manager.GetService(typeof(IEventBindingService)) as IEventBindingService;
						object instance = manager.GetInstance(name);
						if (instance != null && eventBindingService != null)
						{
							PropertyDescriptorCollection eventProperties = eventBindingService.GetEventProperties(TypeDescriptor.GetEvents(instance));
							if (eventProperties != null)
							{
								foreach (string name2 in list)
								{
									PropertyDescriptor propertyDescriptor = eventProperties[name2];
									if (propertyDescriptor != null)
									{
										propertyDescriptor.SetValue(instance, null);
									}
								}
							}
						}
					}
				}

				// Token: 0x060031A4 RID: 12708 RVA: 0x00119340 File Offset: 0x00118340
				private static void DeserializeModifier(IDesignerSerializationManager manager, string name, object state)
				{
					object instance = manager.GetInstance(name);
					if (instance != null)
					{
						MemberAttributes memberAttributes = (MemberAttributes)state;
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(instance)["Modifiers"];
						if (propertyDescriptor != null)
						{
							propertyDescriptor.SetValue(instance, memberAttributes);
						}
					}
				}

				// Token: 0x060031A5 RID: 12709 RVA: 0x00119380 File Offset: 0x00118380
				public override object Serialize(IDesignerSerializationManager manager, object state)
				{
					throw new NotSupportedException();
				}

				// Token: 0x060031A6 RID: 12710 RVA: 0x00119388 File Offset: 0x00118388
				internal void SetupVariableReferences(IDesignerSerializationManager manager, IContainer container, IDictionary objectData, IList shimObjectNames)
				{
					foreach (object obj in container.Components)
					{
						IComponent component = (IComponent)obj;
						string componentName = TypeDescriptor.GetComponentName(component);
						if (componentName != null && componentName.Length > 0)
						{
							bool flag = true;
							if (objectData.Contains(component) && ((CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectData)objectData[component]).EntireObject)
							{
								flag = false;
							}
							if (flag)
							{
								CodeVariableReferenceExpression expression = new CodeVariableReferenceExpression(componentName);
								base.SetExpression(manager, component, expression);
								if (!shimObjectNames.Contains(componentName))
								{
									shimObjectNames.Add(componentName);
								}
								if (component.Site != null)
								{
									INestedContainer nestedContainer = component.Site.GetService(typeof(INestedContainer)) as INestedContainer;
									if (nestedContainer != null && nestedContainer.Components.Count > 0)
									{
										this.SetupVariableReferences(manager, nestedContainer, objectData, shimObjectNames);
									}
								}
							}
						}
					}
				}

				// Token: 0x060031A7 RID: 12711 RVA: 0x0011948C File Offset: 0x0011848C
				internal void Serialize(IDesignerSerializationManager manager, IDictionary objectData, IDictionary objectState, IList shimObjectNames)
				{
					IContainer container = manager.GetService(typeof(IContainer)) as IContainer;
					if (container != null)
					{
						this.SetupVariableReferences(manager, container, objectData, shimObjectNames);
					}
					StatementContext statementContext = new StatementContext();
					statementContext.StatementCollection.Populate(objectData.Keys);
					manager.Context.Push(statementContext);
					try
					{
						foreach (object obj in objectData.Values)
						{
							CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectData objectData2 = (CodeDomComponentSerializationService.CodeDomSerializationStore.ObjectData)obj;
							CodeDomSerializer codeDomSerializer = (CodeDomSerializer)manager.GetSerializer(objectData2.Value.GetType(), typeof(CodeDomSerializer));
							object[] array = new object[6];
							CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
							manager.Context.Push(codeStatementCollection);
							if (codeDomSerializer != null)
							{
								if (objectData2.EntireObject)
								{
									if (!base.IsSerialized(manager, objectData2.Value))
									{
										if (objectData2.Absolute)
										{
											array[0] = codeDomSerializer.SerializeAbsolute(manager, objectData2.Value);
										}
										else
										{
											array[0] = codeDomSerializer.Serialize(manager, objectData2.Value);
										}
										CodeStatementCollection codeStatementCollection2 = statementContext.StatementCollection[objectData2.Value];
										if (codeStatementCollection2 != null && codeStatementCollection2.Count > 0)
										{
											array[1] = codeStatementCollection2;
										}
										if (codeStatementCollection.Count > 0)
										{
											CodeStatementCollection codeStatementCollection3 = array[0] as CodeStatementCollection;
											if (codeStatementCollection3 != null)
											{
												codeStatementCollection3.AddRange(codeStatementCollection);
											}
										}
									}
									else
									{
										array[0] = statementContext.StatementCollection[objectData2.Value];
									}
								}
								else
								{
									CodeStatementCollection codeStatementCollection4 = new CodeStatementCollection();
									foreach (object obj2 in objectData2.Members)
									{
										CodeDomComponentSerializationService.CodeDomSerializationStore.MemberData memberData = (CodeDomComponentSerializationService.CodeDomSerializationStore.MemberData)obj2;
										if (memberData.Member.Attributes.Contains(DesignOnlyAttribute.Yes))
										{
											PropertyDescriptor propertyDescriptor = memberData.Member as PropertyDescriptor;
											if (propertyDescriptor != null && propertyDescriptor.PropertyType.IsSerializable)
											{
												if (array[3] == null)
												{
													array[3] = new Hashtable();
												}
												((Hashtable)array[3])[propertyDescriptor.Name] = propertyDescriptor.GetValue(objectData2.Value);
											}
										}
										else if (memberData.Absolute)
										{
											codeStatementCollection4.AddRange(codeDomSerializer.SerializeMemberAbsolute(manager, objectData2.Value, memberData.Member));
										}
										else
										{
											codeStatementCollection4.AddRange(codeDomSerializer.SerializeMember(manager, objectData2.Value, memberData.Member));
										}
									}
									array[0] = codeStatementCollection4;
								}
							}
							if (codeStatementCollection.Count > 0)
							{
								CodeStatementCollection codeStatementCollection5 = array[0] as CodeStatementCollection;
								if (codeStatementCollection5 != null)
								{
									codeStatementCollection5.AddRange(codeStatementCollection);
								}
							}
							manager.Context.Pop();
							ArrayList arrayList = null;
							List<string> list = null;
							IEventBindingService eventBindingService = manager.GetService(typeof(IEventBindingService)) as IEventBindingService;
							if (!objectData2.EntireObject)
							{
								goto IL_3EA;
							}
							PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(objectData2.Value);
							foreach (object obj3 in properties)
							{
								PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)obj3;
								if (!propertyDescriptor2.ShouldSerializeValue(objectData2.Value) && !propertyDescriptor2.Attributes.Contains(DesignerSerializationVisibilityAttribute.Hidden) && (propertyDescriptor2.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content) || !propertyDescriptor2.IsReadOnly))
								{
									if (arrayList == null)
									{
										arrayList = new ArrayList(objectData2.Members.Count);
									}
									arrayList.Add(propertyDescriptor2.Name);
								}
							}
							if (eventBindingService != null)
							{
								PropertyDescriptorCollection eventProperties = eventBindingService.GetEventProperties(TypeDescriptor.GetEvents(objectData2.Value));
								using (IEnumerator enumerator4 = eventProperties.GetEnumerator())
								{
									while (enumerator4.MoveNext())
									{
										object obj4 = enumerator4.Current;
										PropertyDescriptor propertyDescriptor3 = (PropertyDescriptor)obj4;
										if (propertyDescriptor3 != null && !propertyDescriptor3.IsReadOnly && propertyDescriptor3.GetValue(objectData2.Value) == null)
										{
											if (list == null)
											{
												list = new List<string>();
											}
											list.Add(propertyDescriptor3.Name);
										}
									}
									goto IL_49A;
								}
								goto IL_3EA;
							}
							IL_49A:
							PropertyDescriptor propertyDescriptor4 = TypeDescriptor.GetProperties(objectData2.Value)["Modifiers"];
							if (propertyDescriptor4 != null)
							{
								array[5] = propertyDescriptor4.GetValue(objectData2.Value);
							}
							if (arrayList != null)
							{
								array[2] = (string[])arrayList.ToArray(typeof(string));
							}
							if (list != null)
							{
								array[4] = list;
							}
							if (array[0] != null || array[2] != null)
							{
								objectState[objectData2.Name] = array;
								continue;
							}
							continue;
							IL_3EA:
							foreach (object obj5 in objectData2.Members)
							{
								CodeDomComponentSerializationService.CodeDomSerializationStore.MemberData memberData2 = (CodeDomComponentSerializationService.CodeDomSerializationStore.MemberData)obj5;
								PropertyDescriptor propertyDescriptor5 = memberData2.Member as PropertyDescriptor;
								if (propertyDescriptor5 != null && !propertyDescriptor5.ShouldSerializeValue(objectData2.Value))
								{
									if (eventBindingService != null && eventBindingService.GetEvent(propertyDescriptor5) != null)
									{
										if (list == null)
										{
											list = new List<string>();
										}
										list.Add(propertyDescriptor5.Name);
									}
									else
									{
										if (arrayList == null)
										{
											arrayList = new ArrayList(objectData2.Members.Count);
										}
										arrayList.Add(propertyDescriptor5.Name);
									}
								}
							}
							goto IL_49A;
						}
					}
					finally
					{
						manager.Context.Pop();
					}
				}

				// Token: 0x04002128 RID: 8488
				internal static CodeDomComponentSerializationService.CodeDomSerializationStore.ComponentListCodeDomSerializer Instance = new CodeDomComponentSerializationService.CodeDomSerializationStore.ComponentListCodeDomSerializer();

				// Token: 0x04002129 RID: 8489
				private Hashtable _statementsTable;

				// Token: 0x0400212A RID: 8490
				private Dictionary<string, ArrayList> _expressions;

				// Token: 0x0400212B RID: 8491
				private Hashtable _objectState;

				// Token: 0x0400212C RID: 8492
				private bool applyDefaults = true;

				// Token: 0x0400212D RID: 8493
				private Hashtable _nameResolveGuard = new Hashtable();
			}

			// Token: 0x0200057A RID: 1402
			private class MemberData
			{
				// Token: 0x060031AA RID: 12714 RVA: 0x00119A8E File Offset: 0x00118A8E
				internal MemberData(MemberDescriptor member, bool absolute)
				{
					this.Member = member;
					this.Absolute = absolute;
				}

				// Token: 0x0400212E RID: 8494
				internal MemberDescriptor Member;

				// Token: 0x0400212F RID: 8495
				internal bool Absolute;
			}

			// Token: 0x0200057B RID: 1403
			private class ObjectData
			{
				// Token: 0x17000943 RID: 2371
				// (get) Token: 0x060031AB RID: 12715 RVA: 0x00119AA4 File Offset: 0x00118AA4
				// (set) Token: 0x060031AC RID: 12716 RVA: 0x00119AAC File Offset: 0x00118AAC
				internal bool EntireObject
				{
					get
					{
						return this._entireObject;
					}
					set
					{
						if (value && this._members != null)
						{
							this._members.Clear();
						}
						this._entireObject = value;
					}
				}

				// Token: 0x17000944 RID: 2372
				// (get) Token: 0x060031AD RID: 12717 RVA: 0x00119ACB File Offset: 0x00118ACB
				// (set) Token: 0x060031AE RID: 12718 RVA: 0x00119AD3 File Offset: 0x00118AD3
				internal bool Absolute
				{
					get
					{
						return this._absolute;
					}
					set
					{
						this._absolute = value;
					}
				}

				// Token: 0x17000945 RID: 2373
				// (get) Token: 0x060031AF RID: 12719 RVA: 0x00119ADC File Offset: 0x00118ADC
				internal IList Members
				{
					get
					{
						if (this._members == null)
						{
							this._members = new ArrayList();
						}
						return this._members;
					}
				}

				// Token: 0x04002130 RID: 8496
				private bool _entireObject;

				// Token: 0x04002131 RID: 8497
				private bool _absolute;

				// Token: 0x04002132 RID: 8498
				private ArrayList _members;

				// Token: 0x04002133 RID: 8499
				internal object Value;

				// Token: 0x04002134 RID: 8500
				internal string Name;
			}

			// Token: 0x0200057C RID: 1404
			private class LocalResourceManager : ResourceManager, IResourceWriter, IResourceReader, IEnumerable, IDisposable
			{
				// Token: 0x060031B1 RID: 12721 RVA: 0x00119AFF File Offset: 0x00118AFF
				internal LocalResourceManager()
				{
				}

				// Token: 0x060031B2 RID: 12722 RVA: 0x00119B07 File Offset: 0x00118B07
				internal LocalResourceManager(Hashtable data)
				{
					this._hashtable = data;
				}

				// Token: 0x17000946 RID: 2374
				// (get) Token: 0x060031B3 RID: 12723 RVA: 0x00119B16 File Offset: 0x00118B16
				internal Hashtable Data
				{
					get
					{
						if (this._hashtable == null)
						{
							this._hashtable = new Hashtable();
						}
						return this._hashtable;
					}
				}

				// Token: 0x060031B4 RID: 12724 RVA: 0x00119B31 File Offset: 0x00118B31
				public void AddResource(string name, object value)
				{
					this.Data[name] = value;
				}

				// Token: 0x060031B5 RID: 12725 RVA: 0x00119B40 File Offset: 0x00118B40
				public void AddResource(string name, string value)
				{
					this.Data[name] = value;
				}

				// Token: 0x060031B6 RID: 12726 RVA: 0x00119B4F File Offset: 0x00118B4F
				public void AddResource(string name, byte[] value)
				{
					this.Data[name] = value;
				}

				// Token: 0x060031B7 RID: 12727 RVA: 0x00119B5E File Offset: 0x00118B5E
				public void Close()
				{
				}

				// Token: 0x060031B8 RID: 12728 RVA: 0x00119B60 File Offset: 0x00118B60
				public void Dispose()
				{
					this.Data.Clear();
				}

				// Token: 0x060031B9 RID: 12729 RVA: 0x00119B6D File Offset: 0x00118B6D
				public void Generate()
				{
				}

				// Token: 0x060031BA RID: 12730 RVA: 0x00119B6F File Offset: 0x00118B6F
				public override object GetObject(string name)
				{
					return this.Data[name];
				}

				// Token: 0x060031BB RID: 12731 RVA: 0x00119B7D File Offset: 0x00118B7D
				public override string GetString(string name)
				{
					return this.Data[name] as string;
				}

				// Token: 0x060031BC RID: 12732 RVA: 0x00119B90 File Offset: 0x00118B90
				public IDictionaryEnumerator GetEnumerator()
				{
					return this.Data.GetEnumerator();
				}

				// Token: 0x060031BD RID: 12733 RVA: 0x00119B9D File Offset: 0x00118B9D
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x04002135 RID: 8501
				private Hashtable _hashtable;
			}

			// Token: 0x0200057D RID: 1405
			private class LocalServices : IServiceProvider, IResourceService
			{
				// Token: 0x060031BE RID: 12734 RVA: 0x00119BA5 File Offset: 0x00118BA5
				internal LocalServices(CodeDomComponentSerializationService.CodeDomSerializationStore store, IServiceProvider provider)
				{
					this._store = store;
					this._provider = provider;
				}

				// Token: 0x060031BF RID: 12735 RVA: 0x00119BBB File Offset: 0x00118BBB
				IResourceReader IResourceService.GetResourceReader(CultureInfo info)
				{
					return this._store.Resources;
				}

				// Token: 0x060031C0 RID: 12736 RVA: 0x00119BC8 File Offset: 0x00118BC8
				IResourceWriter IResourceService.GetResourceWriter(CultureInfo info)
				{
					return this._store.Resources;
				}

				// Token: 0x060031C1 RID: 12737 RVA: 0x00119BD5 File Offset: 0x00118BD5
				object IServiceProvider.GetService(Type serviceType)
				{
					if (serviceType == null)
					{
						throw new ArgumentNullException("serviceType");
					}
					if (serviceType == typeof(IResourceService))
					{
						return this;
					}
					if (this._provider != null)
					{
						return this._provider.GetService(serviceType);
					}
					return null;
				}

				// Token: 0x04002136 RID: 8502
				private CodeDomComponentSerializationService.CodeDomSerializationStore _store;

				// Token: 0x04002137 RID: 8503
				private IServiceProvider _provider;
			}

			// Token: 0x0200057E RID: 1406
			private class PassThroughSerializationManager : IDesignerSerializationManager, IServiceProvider
			{
				// Token: 0x060031C2 RID: 12738 RVA: 0x00119C0A File Offset: 0x00118C0A
				public PassThroughSerializationManager(DesignerSerializationManager manager)
				{
					this.manager = manager;
				}

				// Token: 0x17000947 RID: 2375
				// (get) Token: 0x060031C3 RID: 12739 RVA: 0x00119C24 File Offset: 0x00118C24
				public DesignerSerializationManager Manager
				{
					get
					{
						return this.manager;
					}
				}

				// Token: 0x17000948 RID: 2376
				// (get) Token: 0x060031C4 RID: 12740 RVA: 0x00119C2C File Offset: 0x00118C2C
				ContextStack IDesignerSerializationManager.Context
				{
					get
					{
						return ((IDesignerSerializationManager)this.manager).Context;
					}
				}

				// Token: 0x17000949 RID: 2377
				// (get) Token: 0x060031C5 RID: 12741 RVA: 0x00119C39 File Offset: 0x00118C39
				PropertyDescriptorCollection IDesignerSerializationManager.Properties
				{
					get
					{
						return ((IDesignerSerializationManager)this.manager).Properties;
					}
				}

				// Token: 0x14000068 RID: 104
				// (add) Token: 0x060031C6 RID: 12742 RVA: 0x00119C46 File Offset: 0x00118C46
				// (remove) Token: 0x060031C7 RID: 12743 RVA: 0x00119C6B File Offset: 0x00118C6B
				event ResolveNameEventHandler IDesignerSerializationManager.ResolveName
				{
					add
					{
						((IDesignerSerializationManager)this.manager).ResolveName += value;
						this.resolveNameEventHandler = (ResolveNameEventHandler)Delegate.Combine(this.resolveNameEventHandler, value);
					}
					remove
					{
						((IDesignerSerializationManager)this.manager).ResolveName -= value;
						this.resolveNameEventHandler = (ResolveNameEventHandler)Delegate.Remove(this.resolveNameEventHandler, value);
					}
				}

				// Token: 0x14000069 RID: 105
				// (add) Token: 0x060031C8 RID: 12744 RVA: 0x00119C90 File Offset: 0x00118C90
				// (remove) Token: 0x060031C9 RID: 12745 RVA: 0x00119C9E File Offset: 0x00118C9E
				event EventHandler IDesignerSerializationManager.SerializationComplete
				{
					add
					{
						((IDesignerSerializationManager)this.manager).SerializationComplete += value;
					}
					remove
					{
						((IDesignerSerializationManager)this.manager).SerializationComplete -= value;
					}
				}

				// Token: 0x060031CA RID: 12746 RVA: 0x00119CAC File Offset: 0x00118CAC
				void IDesignerSerializationManager.AddSerializationProvider(IDesignerSerializationProvider provider)
				{
					((IDesignerSerializationManager)this.manager).AddSerializationProvider(provider);
				}

				// Token: 0x060031CB RID: 12747 RVA: 0x00119CBA File Offset: 0x00118CBA
				object IDesignerSerializationManager.CreateInstance(Type type, ICollection arguments, string name, bool addToContainer)
				{
					return ((IDesignerSerializationManager)this.manager).CreateInstance(type, arguments, name, addToContainer);
				}

				// Token: 0x060031CC RID: 12748 RVA: 0x00119CCC File Offset: 0x00118CCC
				object IDesignerSerializationManager.GetInstance(string name)
				{
					object instance = ((IDesignerSerializationManager)this.manager).GetInstance(name);
					if (this.resolveNameEventHandler != null && instance != null && !this.resolved.ContainsKey(name) && this.manager.PreserveNames && this.manager.Container != null && this.manager.Container.Components[name] != null)
					{
						this.resolved[name] = true;
						this.resolveNameEventHandler(this, new ResolveNameEventArgs(name));
					}
					return instance;
				}

				// Token: 0x060031CD RID: 12749 RVA: 0x00119D56 File Offset: 0x00118D56
				string IDesignerSerializationManager.GetName(object value)
				{
					return ((IDesignerSerializationManager)this.manager).GetName(value);
				}

				// Token: 0x060031CE RID: 12750 RVA: 0x00119D64 File Offset: 0x00118D64
				object IDesignerSerializationManager.GetSerializer(Type objectType, Type serializerType)
				{
					return ((IDesignerSerializationManager)this.manager).GetSerializer(objectType, serializerType);
				}

				// Token: 0x060031CF RID: 12751 RVA: 0x00119D73 File Offset: 0x00118D73
				Type IDesignerSerializationManager.GetType(string typeName)
				{
					return ((IDesignerSerializationManager)this.manager).GetType(typeName);
				}

				// Token: 0x060031D0 RID: 12752 RVA: 0x00119D81 File Offset: 0x00118D81
				void IDesignerSerializationManager.RemoveSerializationProvider(IDesignerSerializationProvider provider)
				{
					((IDesignerSerializationManager)this.manager).RemoveSerializationProvider(provider);
				}

				// Token: 0x060031D1 RID: 12753 RVA: 0x00119D8F File Offset: 0x00118D8F
				void IDesignerSerializationManager.ReportError(object errorInformation)
				{
					((IDesignerSerializationManager)this.manager).ReportError(errorInformation);
				}

				// Token: 0x060031D2 RID: 12754 RVA: 0x00119D9D File Offset: 0x00118D9D
				void IDesignerSerializationManager.SetName(object instance, string name)
				{
					((IDesignerSerializationManager)this.manager).SetName(instance, name);
				}

				// Token: 0x060031D3 RID: 12755 RVA: 0x00119DAC File Offset: 0x00118DAC
				object IServiceProvider.GetService(Type serviceType)
				{
					return ((IServiceProvider)this.manager).GetService(serviceType);
				}

				// Token: 0x04002138 RID: 8504
				private Hashtable resolved = new Hashtable();

				// Token: 0x04002139 RID: 8505
				private DesignerSerializationManager manager;

				// Token: 0x0400213A RID: 8506
				private ResolveNameEventHandler resolveNameEventHandler;
			}

			// Token: 0x02000583 RID: 1411
			private class LocalDesignerSerializationManager : DesignerSerializationManager
			{
				// Token: 0x06003210 RID: 12816 RVA: 0x0011AC7D File Offset: 0x00119C7D
				internal LocalDesignerSerializationManager(CodeDomComponentSerializationService.CodeDomSerializationStore store, IServiceProvider provider) : base(provider)
				{
					this._store = store;
				}

				// Token: 0x06003211 RID: 12817 RVA: 0x0011AC99 File Offset: 0x00119C99
				protected override object CreateInstance(Type type, ICollection arguments, string name, bool addToContainer)
				{
					if (typeof(ResourceManager).IsAssignableFrom(type))
					{
						return this._store.Resources;
					}
					return base.CreateInstance(type, arguments, name, addToContainer);
				}

				// Token: 0x17000957 RID: 2391
				// (get) Token: 0x06003212 RID: 12818 RVA: 0x0011ACC4 File Offset: 0x00119CC4
				private bool? TypeResolutionAvailable
				{
					get
					{
						if (this._typeSvcAvailable == null)
						{
							this._typeSvcAvailable = new bool?(this.GetService(typeof(ITypeResolutionService)) != null);
						}
						return new bool?(this._typeSvcAvailable.Value);
					}
				}

				// Token: 0x06003213 RID: 12819 RVA: 0x0011AD04 File Offset: 0x00119D04
				protected override Type GetType(string name)
				{
					Type type = base.GetType(name);
					if (((type == null) ? (!this.TypeResolutionAvailable) : new bool?(false)).GetValueOrDefault())
					{
						AssemblyName[] assemblyNames = this._store.AssemblyNames;
						foreach (AssemblyName assemblyRef in assemblyNames)
						{
							Assembly assembly = Assembly.Load(assemblyRef);
							if (assembly != null)
							{
								type = assembly.GetType(name);
								if (type != null)
								{
									break;
								}
							}
						}
						if (type == null)
						{
							foreach (AssemblyName assemblyRef2 in assemblyNames)
							{
								Assembly assembly2 = Assembly.Load(assemblyRef2);
								if (assembly2 != null)
								{
									foreach (AssemblyName assemblyRef3 in assembly2.GetReferencedAssemblies())
									{
										Assembly assembly3 = Assembly.Load(assemblyRef3);
										if (assembly3 != null)
										{
											type = assembly3.GetType(name);
											if (type != null)
											{
												break;
											}
										}
									}
									if (type != null)
									{
										break;
									}
								}
							}
						}
					}
					return type;
				}

				// Token: 0x04002153 RID: 8531
				private CodeDomComponentSerializationService.CodeDomSerializationStore _store;

				// Token: 0x04002154 RID: 8532
				private bool? _typeSvcAvailable = null;
			}
		}
	}
}
