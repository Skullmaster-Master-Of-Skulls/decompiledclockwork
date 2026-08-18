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
	// Token: 0x020001EF RID: 495
	public sealed class CodeDomComponentSerializationService : ComponentSerializationService
	{
		// Token: 0x0600129E RID: 4766 RVA: 0x0006C2A0 File Offset: 0x0006A4A0
		public CodeDomComponentSerializationService() : this(null)
		{
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0006C2A9 File Offset: 0x0006A4A9
		public CodeDomComponentSerializationService(IServiceProvider provider)
		{
			this._provider = provider;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0006C2B8 File Offset: 0x0006A4B8
		public override SerializationStore CreateStore()
		{
			return new CodeDomComponentSerializationService.CodeDomSerializationStore(this._provider);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0006C2C5 File Offset: 0x0006A4C5
		public override SerializationStore LoadStore(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return CodeDomComponentSerializationService.CodeDomSerializationStore.Load(stream);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0006C2DC File Offset: 0x0006A4DC
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

		// Token: 0x060012A3 RID: 4771 RVA: 0x0006C328 File Offset: 0x0006A528
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

		// Token: 0x060012A4 RID: 4772 RVA: 0x0006C374 File Offset: 0x0006A574
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

		// Token: 0x060012A5 RID: 4773 RVA: 0x0006C3D0 File Offset: 0x0006A5D0
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

		// Token: 0x060012A6 RID: 4774 RVA: 0x0006C42C File Offset: 0x0006A62C
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

		// Token: 0x060012A7 RID: 4775 RVA: 0x0006C470 File Offset: 0x0006A670
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

		// Token: 0x060012A8 RID: 4776 RVA: 0x0006C4C0 File Offset: 0x0006A6C0
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

		// Token: 0x04000A23 RID: 2595
		private IServiceProvider _provider;

		// Token: 0x020004B0 RID: 1200
		[Serializable]
		private sealed class CodeDomSerializationStore : SerializationStore, ISerializable
		{
			// Token: 0x06002BDF RID: 11231 RVA: 0x00105ABC File Offset: 0x00103CBC
			internal CodeDomSerializationStore(IServiceProvider provider)
			{
				this._provider = provider;
				this._objects = new Hashtable();
				this._objectNames = new ArrayList();
				this._shimObjectNames = new List<string>();
			}

			// Token: 0x06002BE0 RID: 11232 RVA: 0x00105AEC File Offset: 0x00103CEC
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

			// Token: 0x17000949 RID: 2377
			// (get) Token: 0x06002BE1 RID: 11233 RVA: 0x00105BA9 File Offset: 0x00103DA9
			private AssemblyName[] AssemblyNames
			{
				get
				{
					return this._assemblies;
				}
			}

			// Token: 0x1700094A RID: 2378
			// (get) Token: 0x06002BE2 RID: 11234 RVA: 0x00105BB4 File Offset: 0x00103DB4
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

			// Token: 0x1700094B RID: 2379
			// (get) Token: 0x06002BE3 RID: 11235 RVA: 0x00105BF4 File Offset: 0x00103DF4
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

			// Token: 0x06002BE4 RID: 11236 RVA: 0x00105C10 File Offset: 0x00103E10
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

			// Token: 0x06002BE5 RID: 11237 RVA: 0x00105C98 File Offset: 0x00103E98
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

			// Token: 0x06002BE6 RID: 11238 RVA: 0x00105D1C File Offset: 0x00103F1C
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

			// Token: 0x06002BE7 RID: 11239 RVA: 0x00105FA0 File Offset: 0x001041A0
			internal ICollection Deserialize(IServiceProvider provider)
			{
				return this.Deserialize(provider, null, false, true, true);
			}

			// Token: 0x06002BE8 RID: 11240 RVA: 0x00105FAD File Offset: 0x001041AD
			internal ICollection Deserialize(IServiceProvider provider, IContainer container)
			{
				return this.Deserialize(provider, container, false, true, true);
			}

			// Token: 0x06002BE9 RID: 11241 RVA: 0x00105FBC File Offset: 0x001041BC
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

			// Token: 0x06002BEA RID: 11242 RVA: 0x00106210 File Offset: 0x00104410
			internal void DeserializeTo(IServiceProvider provider, IContainer container, bool validateRecycledTypes, bool applyDefaults)
			{
				this.Deserialize(provider, container, true, validateRecycledTypes, applyDefaults);
			}

			// Token: 0x06002BEB RID: 11243 RVA: 0x00106220 File Offset: 0x00104420
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

			// Token: 0x06002BEC RID: 11244 RVA: 0x001062B8 File Offset: 0x001044B8
			internal static CodeDomComponentSerializationService.CodeDomSerializationStore Load(Stream stream)
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				return (CodeDomComponentSerializationService.CodeDomSerializationStore)binaryFormatter.Deserialize(stream);
			}

			// Token: 0x06002BED RID: 11245 RVA: 0x001062D8 File Offset: 0x001044D8
			public override void Save(Stream stream)
			{
				this.Close();
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(stream, this);
			}

			// Token: 0x06002BEE RID: 11246 RVA: 0x00003937 File Offset: 0x00001B37
			[Conditional("DEBUG")]
			internal static void Trace(string message, params object[] args)
			{
			}

			// Token: 0x06002BEF RID: 11247 RVA: 0x001062FC File Offset: 0x001044FC
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

			// Token: 0x04001E72 RID: 7794
			private const string _stateKey = "State";

			// Token: 0x04001E73 RID: 7795
			private const string _nameKey = "Names";

			// Token: 0x04001E74 RID: 7796
			private const string _assembliesKey = "Assemblies";

			// Token: 0x04001E75 RID: 7797
			private const string _resourcesKey = "Resources";

			// Token: 0x04001E76 RID: 7798
			private const string _shimKey = "Shim";

			// Token: 0x04001E77 RID: 7799
			private const int _stateCode = 0;

			// Token: 0x04001E78 RID: 7800
			private const int _stateCtx = 1;

			// Token: 0x04001E79 RID: 7801
			private const int _stateProperties = 2;

			// Token: 0x04001E7A RID: 7802
			private const int _stateResources = 3;

			// Token: 0x04001E7B RID: 7803
			private const int _stateEvents = 4;

			// Token: 0x04001E7C RID: 7804
			private const int _stateModifier = 5;

			// Token: 0x04001E7D RID: 7805
			private MemoryStream _resourceStream;

			// Token: 0x04001E7E RID: 7806
			private Hashtable _objects;

			// Token: 0x04001E7F RID: 7807
			private IServiceProvider _provider;

			// Token: 0x04001E80 RID: 7808
			private ArrayList _objectNames;

			// Token: 0x04001E81 RID: 7809
			private Hashtable _objectState;

			// Token: 0x04001E82 RID: 7810
			private CodeDomComponentSerializationService.CodeDomSerializationStore.LocalResourceManager _resources;

			// Token: 0x04001E83 RID: 7811
			private AssemblyName[] _assemblies;

			// Token: 0x04001E84 RID: 7812
			private List<string> _shimObjectNames;

			// Token: 0x04001E85 RID: 7813
			private ICollection _errors;

			// Token: 0x020005DF RID: 1503
			private class ComponentListCodeDomSerializer : CodeDomSerializer
			{
				// Token: 0x06003481 RID: 13441 RVA: 0x0000C5AC File Offset: 0x0000A7AC
				public override object Deserialize(IDesignerSerializationManager manager, object state)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003482 RID: 13442 RVA: 0x0011D490 File Offset: 0x0011B690
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

				// Token: 0x06003483 RID: 13443 RVA: 0x0011D508 File Offset: 0x0011B708
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

				// Token: 0x06003484 RID: 13444 RVA: 0x0011D740 File Offset: 0x0011B940
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

				// Token: 0x06003485 RID: 13445 RVA: 0x0011D7C8 File Offset: 0x0011B9C8
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

				// Token: 0x06003486 RID: 13446 RVA: 0x0011D87C File Offset: 0x0011BA7C
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

				// Token: 0x06003487 RID: 13447 RVA: 0x0011D910 File Offset: 0x0011BB10
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

				// Token: 0x06003488 RID: 13448 RVA: 0x0011D9F0 File Offset: 0x0011BBF0
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
								goto IL_1CE;
							}
							if (orderedCodeStatementCollection == null || orderedCodeStatementCollection.Count <= 0)
							{
								goto IL_1CE;
							}
							CodeDomSerializer serializer = base.GetSerializer(manager, type);
							if (serializer == null)
							{
								manager.ReportError(new CodeDomSerializerException(SR.GetString("SerializerNoSerializerForComponent", new object[]
								{
									type.FullName
								}), manager));
								goto IL_1CE;
							}
							try
							{
								object obj2 = serializer.Deserialize(manager, orderedCodeStatementCollection);
								flag = (obj2 != null);
								if (flag)
								{
									this._statementsTable[name] = obj2;
								}
								goto IL_1CE;
							}
							catch (Exception errorInformation)
							{
								manager.ReportError(errorInformation);
								goto IL_1CE;
							}
						}
						foreach (object obj3 in orderedCodeStatementCollection)
						{
							CodeStatement statement = (CodeStatement)obj3;
							base.DeserializeStatement(manager, statement);
						}
						flag = true;
						IL_1CE:
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
								object obj5 = base.DeserializeExpression(manager, name, expression);
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
								foreach (object obj6 in arrayList2)
								{
									CodeExpression expression2 = (CodeExpression)obj6;
									object obj7 = base.DeserializeExpression(manager, name, expression2);
									if (obj7 != null && !flag && canInvokeManager && manager.GetInstance(name) == null)
									{
										manager.SetName(obj7, name);
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

				// Token: 0x06003489 RID: 13449 RVA: 0x0011DDF8 File Offset: 0x0011BFF8
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

				// Token: 0x0600348A RID: 13450 RVA: 0x0011DEA4 File Offset: 0x0011C0A4
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

				// Token: 0x0600348B RID: 13451 RVA: 0x0000C5AC File Offset: 0x0000A7AC
				public override object Serialize(IDesignerSerializationManager manager, object state)
				{
					throw new NotSupportedException();
				}

				// Token: 0x0600348C RID: 13452 RVA: 0x0011DEE4 File Offset: 0x0011C0E4
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

				// Token: 0x0600348D RID: 13453 RVA: 0x0011DFE4 File Offset: 0x0011C1E4
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
								goto IL_3EE;
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
									goto IL_49E;
								}
								goto IL_3EE;
							}
							IL_49E:
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
							IL_3EE:
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
							goto IL_49E;
						}
					}
					finally
					{
						manager.Context.Pop();
					}
				}

				// Token: 0x0400231C RID: 8988
				internal static CodeDomComponentSerializationService.CodeDomSerializationStore.ComponentListCodeDomSerializer Instance = new CodeDomComponentSerializationService.CodeDomSerializationStore.ComponentListCodeDomSerializer();

				// Token: 0x0400231D RID: 8989
				private Hashtable _statementsTable;

				// Token: 0x0400231E RID: 8990
				private Dictionary<string, ArrayList> _expressions;

				// Token: 0x0400231F RID: 8991
				private Hashtable _objectState;

				// Token: 0x04002320 RID: 8992
				private bool applyDefaults = true;

				// Token: 0x04002321 RID: 8993
				private Hashtable _nameResolveGuard = new Hashtable();
			}

			// Token: 0x020005E0 RID: 1504
			private class MemberData
			{
				// Token: 0x06003490 RID: 13456 RVA: 0x0011E5E6 File Offset: 0x0011C7E6
				internal MemberData(MemberDescriptor member, bool absolute)
				{
					this.Member = member;
					this.Absolute = absolute;
				}

				// Token: 0x04002322 RID: 8994
				internal MemberDescriptor Member;

				// Token: 0x04002323 RID: 8995
				internal bool Absolute;
			}

			// Token: 0x020005E1 RID: 1505
			private class ObjectData
			{
				// Token: 0x17000A2A RID: 2602
				// (get) Token: 0x06003491 RID: 13457 RVA: 0x0011E5FC File Offset: 0x0011C7FC
				// (set) Token: 0x06003492 RID: 13458 RVA: 0x0011E604 File Offset: 0x0011C804
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

				// Token: 0x17000A2B RID: 2603
				// (get) Token: 0x06003493 RID: 13459 RVA: 0x0011E623 File Offset: 0x0011C823
				// (set) Token: 0x06003494 RID: 13460 RVA: 0x0011E62B File Offset: 0x0011C82B
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

				// Token: 0x17000A2C RID: 2604
				// (get) Token: 0x06003495 RID: 13461 RVA: 0x0011E634 File Offset: 0x0011C834
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

				// Token: 0x04002324 RID: 8996
				private bool _entireObject;

				// Token: 0x04002325 RID: 8997
				private bool _absolute;

				// Token: 0x04002326 RID: 8998
				private ArrayList _members;

				// Token: 0x04002327 RID: 8999
				internal object Value;

				// Token: 0x04002328 RID: 9000
				internal string Name;
			}

			// Token: 0x020005E2 RID: 1506
			private class LocalResourceManager : ResourceManager, IResourceWriter, IDisposable, IResourceReader, IEnumerable
			{
				// Token: 0x06003497 RID: 13463 RVA: 0x0011E64F File Offset: 0x0011C84F
				internal LocalResourceManager()
				{
				}

				// Token: 0x06003498 RID: 13464 RVA: 0x0011E657 File Offset: 0x0011C857
				internal LocalResourceManager(Hashtable data)
				{
					this._hashtable = data;
				}

				// Token: 0x17000A2D RID: 2605
				// (get) Token: 0x06003499 RID: 13465 RVA: 0x0011E666 File Offset: 0x0011C866
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

				// Token: 0x0600349A RID: 13466 RVA: 0x0011E681 File Offset: 0x0011C881
				public void AddResource(string name, object value)
				{
					this.Data[name] = value;
				}

				// Token: 0x0600349B RID: 13467 RVA: 0x0011E681 File Offset: 0x0011C881
				public void AddResource(string name, string value)
				{
					this.Data[name] = value;
				}

				// Token: 0x0600349C RID: 13468 RVA: 0x0011E681 File Offset: 0x0011C881
				public void AddResource(string name, byte[] value)
				{
					this.Data[name] = value;
				}

				// Token: 0x0600349D RID: 13469 RVA: 0x00003937 File Offset: 0x00001B37
				public void Close()
				{
				}

				// Token: 0x0600349E RID: 13470 RVA: 0x0011E690 File Offset: 0x0011C890
				public void Dispose()
				{
					this.Data.Clear();
				}

				// Token: 0x0600349F RID: 13471 RVA: 0x00003937 File Offset: 0x00001B37
				public void Generate()
				{
				}

				// Token: 0x060034A0 RID: 13472 RVA: 0x0011E69D File Offset: 0x0011C89D
				public override object GetObject(string name)
				{
					return this.Data[name];
				}

				// Token: 0x060034A1 RID: 13473 RVA: 0x0011E6AB File Offset: 0x0011C8AB
				public override string GetString(string name)
				{
					return this.Data[name] as string;
				}

				// Token: 0x060034A2 RID: 13474 RVA: 0x0011E6BE File Offset: 0x0011C8BE
				public IDictionaryEnumerator GetEnumerator()
				{
					return this.Data.GetEnumerator();
				}

				// Token: 0x060034A3 RID: 13475 RVA: 0x0011E6CB File Offset: 0x0011C8CB
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x04002329 RID: 9001
				private Hashtable _hashtable;
			}

			// Token: 0x020005E3 RID: 1507
			private class LocalServices : IServiceProvider, IResourceService
			{
				// Token: 0x060034A4 RID: 13476 RVA: 0x0011E6D3 File Offset: 0x0011C8D3
				internal LocalServices(CodeDomComponentSerializationService.CodeDomSerializationStore store, IServiceProvider provider)
				{
					this._store = store;
					this._provider = provider;
				}

				// Token: 0x060034A5 RID: 13477 RVA: 0x0011E6E9 File Offset: 0x0011C8E9
				IResourceReader IResourceService.GetResourceReader(CultureInfo info)
				{
					return this._store.Resources;
				}

				// Token: 0x060034A6 RID: 13478 RVA: 0x0011E6E9 File Offset: 0x0011C8E9
				IResourceWriter IResourceService.GetResourceWriter(CultureInfo info)
				{
					return this._store.Resources;
				}

				// Token: 0x060034A7 RID: 13479 RVA: 0x0011E6F6 File Offset: 0x0011C8F6
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

				// Token: 0x0400232A RID: 9002
				private CodeDomComponentSerializationService.CodeDomSerializationStore _store;

				// Token: 0x0400232B RID: 9003
				private IServiceProvider _provider;
			}

			// Token: 0x020005E4 RID: 1508
			private class PassThroughSerializationManager : IDesignerSerializationManager, IServiceProvider
			{
				// Token: 0x060034A8 RID: 13480 RVA: 0x0011E736 File Offset: 0x0011C936
				public PassThroughSerializationManager(DesignerSerializationManager manager)
				{
					this.manager = manager;
				}

				// Token: 0x17000A2E RID: 2606
				// (get) Token: 0x060034A9 RID: 13481 RVA: 0x0011E750 File Offset: 0x0011C950
				public DesignerSerializationManager Manager
				{
					get
					{
						return this.manager;
					}
				}

				// Token: 0x17000A2F RID: 2607
				// (get) Token: 0x060034AA RID: 13482 RVA: 0x0011E758 File Offset: 0x0011C958
				ContextStack IDesignerSerializationManager.Context
				{
					get
					{
						return ((IDesignerSerializationManager)this.manager).Context;
					}
				}

				// Token: 0x17000A30 RID: 2608
				// (get) Token: 0x060034AB RID: 13483 RVA: 0x0011E765 File Offset: 0x0011C965
				PropertyDescriptorCollection IDesignerSerializationManager.Properties
				{
					get
					{
						return ((IDesignerSerializationManager)this.manager).Properties;
					}
				}

				// Token: 0x1400006C RID: 108
				// (add) Token: 0x060034AC RID: 13484 RVA: 0x0011E772 File Offset: 0x0011C972
				// (remove) Token: 0x060034AD RID: 13485 RVA: 0x0011E797 File Offset: 0x0011C997
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

				// Token: 0x1400006D RID: 109
				// (add) Token: 0x060034AE RID: 13486 RVA: 0x0011E7BC File Offset: 0x0011C9BC
				// (remove) Token: 0x060034AF RID: 13487 RVA: 0x0011E7CA File Offset: 0x0011C9CA
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

				// Token: 0x060034B0 RID: 13488 RVA: 0x0011E7D8 File Offset: 0x0011C9D8
				void IDesignerSerializationManager.AddSerializationProvider(IDesignerSerializationProvider provider)
				{
					((IDesignerSerializationManager)this.manager).AddSerializationProvider(provider);
				}

				// Token: 0x060034B1 RID: 13489 RVA: 0x0011E7E6 File Offset: 0x0011C9E6
				object IDesignerSerializationManager.CreateInstance(Type type, ICollection arguments, string name, bool addToContainer)
				{
					return ((IDesignerSerializationManager)this.manager).CreateInstance(type, arguments, name, addToContainer);
				}

				// Token: 0x060034B2 RID: 13490 RVA: 0x0011E7F8 File Offset: 0x0011C9F8
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

				// Token: 0x060034B3 RID: 13491 RVA: 0x0011E882 File Offset: 0x0011CA82
				string IDesignerSerializationManager.GetName(object value)
				{
					return ((IDesignerSerializationManager)this.manager).GetName(value);
				}

				// Token: 0x060034B4 RID: 13492 RVA: 0x0011E890 File Offset: 0x0011CA90
				object IDesignerSerializationManager.GetSerializer(Type objectType, Type serializerType)
				{
					return ((IDesignerSerializationManager)this.manager).GetSerializer(objectType, serializerType);
				}

				// Token: 0x060034B5 RID: 13493 RVA: 0x0011E89F File Offset: 0x0011CA9F
				Type IDesignerSerializationManager.GetType(string typeName)
				{
					return ((IDesignerSerializationManager)this.manager).GetType(typeName);
				}

				// Token: 0x060034B6 RID: 13494 RVA: 0x0011E8AD File Offset: 0x0011CAAD
				void IDesignerSerializationManager.RemoveSerializationProvider(IDesignerSerializationProvider provider)
				{
					((IDesignerSerializationManager)this.manager).RemoveSerializationProvider(provider);
				}

				// Token: 0x060034B7 RID: 13495 RVA: 0x0011E8BB File Offset: 0x0011CABB
				void IDesignerSerializationManager.ReportError(object errorInformation)
				{
					((IDesignerSerializationManager)this.manager).ReportError(errorInformation);
				}

				// Token: 0x060034B8 RID: 13496 RVA: 0x0011E8C9 File Offset: 0x0011CAC9
				void IDesignerSerializationManager.SetName(object instance, string name)
				{
					((IDesignerSerializationManager)this.manager).SetName(instance, name);
				}

				// Token: 0x060034B9 RID: 13497 RVA: 0x0011E8D8 File Offset: 0x0011CAD8
				object IServiceProvider.GetService(Type serviceType)
				{
					return ((IServiceProvider)this.manager).GetService(serviceType);
				}

				// Token: 0x0400232C RID: 9004
				private Hashtable resolved = new Hashtable();

				// Token: 0x0400232D RID: 9005
				private DesignerSerializationManager manager;

				// Token: 0x0400232E RID: 9006
				private ResolveNameEventHandler resolveNameEventHandler;
			}

			// Token: 0x020005E5 RID: 1509
			private class LocalDesignerSerializationManager : DesignerSerializationManager
			{
				// Token: 0x060034BA RID: 13498 RVA: 0x0011E8E6 File Offset: 0x0011CAE6
				internal LocalDesignerSerializationManager(CodeDomComponentSerializationService.CodeDomSerializationStore store, IServiceProvider provider) : base(provider)
				{
					this._store = store;
				}

				// Token: 0x060034BB RID: 13499 RVA: 0x0011E8F6 File Offset: 0x0011CAF6
				protected override object CreateInstance(Type type, ICollection arguments, string name, bool addToContainer)
				{
					if (typeof(ResourceManager).IsAssignableFrom(type))
					{
						return this._store.Resources;
					}
					return base.CreateInstance(type, arguments, name, addToContainer);
				}

				// Token: 0x17000A31 RID: 2609
				// (get) Token: 0x060034BC RID: 13500 RVA: 0x0011E921 File Offset: 0x0011CB21
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

				// Token: 0x060034BD RID: 13501 RVA: 0x0011E960 File Offset: 0x0011CB60
				protected override Type GetType(string name)
				{
					Type type = base.GetType(name);
					if (type == null && !this.TypeResolutionAvailable.Value)
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

				// Token: 0x0400232F RID: 9007
				private CodeDomComponentSerializationService.CodeDomSerializationStore _store;

				// Token: 0x04002330 RID: 9008
				private bool? _typeSvcAvailable;
			}
		}
	}
}
