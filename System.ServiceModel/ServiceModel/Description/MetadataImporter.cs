using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime;
using System.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Xml;

namespace System.ServiceModel.Description
{
	// Token: 0x02000401 RID: 1025
	public abstract class MetadataImporter
	{
		// Token: 0x06002724 RID: 10020 RVA: 0x00091294 File Offset: 0x0008F494
		internal static IEnumerable<PolicyConversionContext> GetPolicyConversionContextEnumerator(ServiceEndpoint endpoint, MetadataImporter.PolicyAlternatives policyAlternatives)
		{
			return MetadataImporter.ImportedPolicyConversionContext.GetPolicyConversionContextEnumerator(endpoint, policyAlternatives, MetadataImporterQuotas.Defaults);
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x000912A2 File Offset: 0x0008F4A2
		internal static IEnumerable<PolicyConversionContext> GetPolicyConversionContextEnumerator(ServiceEndpoint endpoint, MetadataImporter.PolicyAlternatives policyAlternatives, MetadataImporterQuotas quotas)
		{
			return MetadataImporter.ImportedPolicyConversionContext.GetPolicyConversionContextEnumerator(endpoint, policyAlternatives, quotas);
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000912AC File Offset: 0x0008F4AC
		internal MetadataImporter() : this(null, MetadataImporterQuotas.Defaults)
		{
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000912BA File Offset: 0x0008F4BA
		internal MetadataImporter(IEnumerable<IPolicyImportExtension> policyImportExtensions) : this(policyImportExtensions, MetadataImporterQuotas.Defaults)
		{
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x000912C8 File Offset: 0x0008F4C8
		internal MetadataImporter(IEnumerable<IPolicyImportExtension> policyImportExtensions, MetadataImporterQuotas quotas)
		{
			if (quotas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("quotas");
			}
			if (policyImportExtensions == null)
			{
				policyImportExtensions = MetadataImporter.LoadPolicyExtensionsFromConfig();
			}
			this.Quotas = quotas;
			this.policyExtensions = new KeyedByTypeCollection<IPolicyImportExtension>(policyImportExtensions);
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x0009132C File Offset: 0x0008F52C
		public KeyedByTypeCollection<IPolicyImportExtension> PolicyImportExtensions
		{
			get
			{
				return this.policyExtensions;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x0600272A RID: 10026 RVA: 0x00091334 File Offset: 0x0008F534
		public Collection<MetadataConversionError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x0009133C File Offset: 0x0008F53C
		public Dictionary<object, object> State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x00091344 File Offset: 0x0008F544
		public Dictionary<XmlQualifiedName, ContractDescription> KnownContracts
		{
			get
			{
				return this.knownContracts;
			}
		}

		// Token: 0x0600272D RID: 10029
		public abstract Collection<ContractDescription> ImportAllContracts();

		// Token: 0x0600272E RID: 10030
		public abstract ServiceEndpointCollection ImportAllEndpoints();

		// Token: 0x0600272F RID: 10031 RVA: 0x0009134C File Offset: 0x0008F54C
		internal virtual XmlElement ResolvePolicyReference(string policyReference, XmlElement contextAssertion)
		{
			return null;
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x00091350 File Offset: 0x0008F550
		internal BindingElementCollection ImportPolicy(ServiceEndpoint endpoint, Collection<Collection<XmlElement>> policyAlternatives)
		{
			foreach (Collection<XmlElement> bindingPolicy in policyAlternatives)
			{
				MetadataImporter.BindingOnlyPolicyConversionContext bindingOnlyPolicyConversionContext = new MetadataImporter.BindingOnlyPolicyConversionContext(endpoint, bindingPolicy);
				if (this.TryImportPolicy(bindingOnlyPolicyConversionContext))
				{
					return bindingOnlyPolicyConversionContext.BindingElements;
				}
			}
			return null;
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x000913B0 File Offset: 0x0008F5B0
		internal bool TryImportPolicy(PolicyConversionContext policyContext)
		{
			foreach (IPolicyImportExtension policyImportExtension in this.policyExtensions)
			{
				try
				{
					policyImportExtension.ImportPolicy(this, policyContext);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.CreateExtensionException(policyImportExtension, ex));
				}
			}
			if (policyContext.GetBindingAssertions().Count != 0)
			{
				return false;
			}
			foreach (OperationDescription operationDescription in policyContext.Contract.Operations)
			{
				if (policyContext.GetOperationBindingAssertions(operationDescription).Count != 0)
				{
					return false;
				}
				foreach (MessageDescription message in operationDescription.Messages)
				{
					if (policyContext.GetMessageBindingAssertions(message).Count != 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x000914DC File Offset: 0x0008F6DC
		[SecuritySafeCritical]
		private static Collection<IPolicyImportExtension> LoadPolicyExtensionsFromConfig()
		{
			return ClientSection.UnsafeGetSection().Metadata.LoadPolicyImportExtensions();
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x000914F0 File Offset: 0x0008F6F0
		private Exception CreateExtensionException(IPolicyImportExtension importer, Exception e)
		{
			string @string = SR.GetString("PolicyExtensionImportError", new object[]
			{
				importer.GetType(),
				e.Message
			});
			return new InvalidOperationException(@string, e);
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06002734 RID: 10036 RVA: 0x00091528 File Offset: 0x0008F728
		// (remove) Token: 0x06002735 RID: 10037 RVA: 0x00091560 File Offset: 0x0008F760
		internal event MetadataImporter.PolicyWarningHandler PolicyWarningOccured;

		// Token: 0x06002736 RID: 10038 RVA: 0x00091595 File Offset: 0x0008F795
		internal IEnumerable<IEnumerable<XmlElement>> NormalizePolicy(IEnumerable<XmlElement> policyAssertions)
		{
			if (this.policyNormalizer == null)
			{
				this.policyNormalizer = new MetadataImporter.PolicyReader(this);
			}
			return this.policyNormalizer.NormalizePolicy(policyAssertions);
		}

		// Token: 0x040021DB RID: 8667
		private readonly KeyedByTypeCollection<IPolicyImportExtension> policyExtensions;

		// Token: 0x040021DC RID: 8668
		private readonly Dictionary<XmlQualifiedName, ContractDescription> knownContracts = new Dictionary<XmlQualifiedName, ContractDescription>();

		// Token: 0x040021DD RID: 8669
		private readonly Collection<MetadataConversionError> errors = new Collection<MetadataConversionError>();

		// Token: 0x040021DE RID: 8670
		private readonly Dictionary<object, object> state = new Dictionary<object, object>();

		// Token: 0x040021DF RID: 8671
		internal MetadataImporterQuotas Quotas;

		// Token: 0x040021E0 RID: 8672
		private MetadataImporter.PolicyReader policyNormalizer;

		// Token: 0x02000BB8 RID: 3000
		internal sealed class ImportedPolicyConversionContext : PolicyConversionContext
		{
			// Token: 0x06007450 RID: 29776 RVA: 0x001B22F4 File Offset: 0x001B04F4
			private ImportedPolicyConversionContext(ServiceEndpoint endpoint, IEnumerable<XmlElement> endpointAssertions, Dictionary<OperationDescription, IEnumerable<XmlElement>> operationBindingAssertions, Dictionary<MessageDescription, IEnumerable<XmlElement>> messageBindingAssertions, Dictionary<FaultDescription, IEnumerable<XmlElement>> faultBindingAssertions, MetadataImporterQuotas quotas) : base(endpoint)
			{
				int num = quotas.MaxPolicyAssertions;
				this.endpointAssertions = new PolicyAssertionCollection(new MetadataImporter.ImportedPolicyConversionContext.MaxItemsEnumerable<XmlElement>(endpointAssertions, num));
				num -= this.endpointAssertions.Count;
				foreach (OperationDescription operationDescription in endpoint.Contract.Operations)
				{
					this.operationBindingAssertions.Add(operationDescription, new PolicyAssertionCollection());
					foreach (MessageDescription key in operationDescription.Messages)
					{
						this.messageBindingAssertions.Add(key, new PolicyAssertionCollection());
					}
					foreach (FaultDescription key2 in operationDescription.Faults)
					{
						this.faultBindingAssertions.Add(key2, new PolicyAssertionCollection());
					}
				}
				foreach (KeyValuePair<OperationDescription, IEnumerable<XmlElement>> keyValuePair in operationBindingAssertions)
				{
					this.operationBindingAssertions[keyValuePair.Key].AddRange(new MetadataImporter.ImportedPolicyConversionContext.MaxItemsEnumerable<XmlElement>(keyValuePair.Value, num));
					num -= this.operationBindingAssertions[keyValuePair.Key].Count;
				}
				foreach (KeyValuePair<MessageDescription, IEnumerable<XmlElement>> keyValuePair2 in messageBindingAssertions)
				{
					this.messageBindingAssertions[keyValuePair2.Key].AddRange(new MetadataImporter.ImportedPolicyConversionContext.MaxItemsEnumerable<XmlElement>(keyValuePair2.Value, num));
					num -= this.messageBindingAssertions[keyValuePair2.Key].Count;
				}
				foreach (KeyValuePair<FaultDescription, IEnumerable<XmlElement>> keyValuePair3 in faultBindingAssertions)
				{
					this.faultBindingAssertions[keyValuePair3.Key].AddRange(new MetadataImporter.ImportedPolicyConversionContext.MaxItemsEnumerable<XmlElement>(keyValuePair3.Value, num));
					num -= this.faultBindingAssertions[keyValuePair3.Key].Count;
				}
			}

			// Token: 0x17001AE5 RID: 6885
			// (get) Token: 0x06007451 RID: 29777 RVA: 0x001B25AC File Offset: 0x001B07AC
			public override BindingElementCollection BindingElements
			{
				get
				{
					return this.bindingElements;
				}
			}

			// Token: 0x06007452 RID: 29778 RVA: 0x001B25B4 File Offset: 0x001B07B4
			public override PolicyAssertionCollection GetBindingAssertions()
			{
				return this.endpointAssertions;
			}

			// Token: 0x06007453 RID: 29779 RVA: 0x001B25BC File Offset: 0x001B07BC
			public override PolicyAssertionCollection GetOperationBindingAssertions(OperationDescription operation)
			{
				return this.operationBindingAssertions[operation];
			}

			// Token: 0x06007454 RID: 29780 RVA: 0x001B25CA File Offset: 0x001B07CA
			public override PolicyAssertionCollection GetMessageBindingAssertions(MessageDescription message)
			{
				return this.messageBindingAssertions[message];
			}

			// Token: 0x06007455 RID: 29781 RVA: 0x001B25D8 File Offset: 0x001B07D8
			public override PolicyAssertionCollection GetFaultBindingAssertions(FaultDescription message)
			{
				return this.faultBindingAssertions[message];
			}

			// Token: 0x06007456 RID: 29782 RVA: 0x001B25E6 File Offset: 0x001B07E6
			public static IEnumerable<PolicyConversionContext> GetPolicyConversionContextEnumerator(ServiceEndpoint endpoint, MetadataImporter.PolicyAlternatives policyAlternatives, MetadataImporterQuotas quotas)
			{
				IEnumerable<Dictionary<FaultDescription, IEnumerable<XmlElement>>> cartesianProduct = MetadataImporter.ImportedPolicyConversionContext.PolicyIterationHelper.GetCartesianProduct<FaultDescription, IEnumerable<XmlElement>>(policyAlternatives.FaultBindingAlternatives);
				IEnumerable<Dictionary<MessageDescription, IEnumerable<XmlElement>>> messageAssertionEnumerator = MetadataImporter.ImportedPolicyConversionContext.PolicyIterationHelper.GetCartesianProduct<MessageDescription, IEnumerable<XmlElement>>(policyAlternatives.MessageBindingAlternatives);
				IEnumerable<Dictionary<OperationDescription, IEnumerable<XmlElement>>> operationAssertionEnumerator = MetadataImporter.ImportedPolicyConversionContext.PolicyIterationHelper.GetCartesianProduct<OperationDescription, IEnumerable<XmlElement>>(policyAlternatives.OperationBindingAlternatives);
				foreach (Dictionary<FaultDescription, IEnumerable<XmlElement>> faultAssertionsSelection in cartesianProduct)
				{
					foreach (Dictionary<MessageDescription, IEnumerable<XmlElement>> messageAssertionsSelection in messageAssertionEnumerator)
					{
						foreach (Dictionary<OperationDescription, IEnumerable<XmlElement>> operationAssertionsSelection in operationAssertionEnumerator)
						{
							foreach (IEnumerable<XmlElement> enumerable in policyAlternatives.EndpointAlternatives)
							{
								MetadataImporter.ImportedPolicyConversionContext importedPolicyConversionContext;
								try
								{
									importedPolicyConversionContext = new MetadataImporter.ImportedPolicyConversionContext(endpoint, enumerable, operationAssertionsSelection, messageAssertionsSelection, faultAssertionsSelection, quotas);
								}
								catch (MetadataImporter.ImportedPolicyConversionContext.MaxItemsEnumeratorExceededMaxItemsException)
								{
									yield break;
								}
								yield return importedPolicyConversionContext;
							}
							IEnumerator<IEnumerable<XmlElement>> enumerator4 = null;
							operationAssertionsSelection = null;
						}
						IEnumerator<Dictionary<OperationDescription, IEnumerable<XmlElement>>> enumerator3 = null;
						messageAssertionsSelection = null;
					}
					IEnumerator<Dictionary<MessageDescription, IEnumerable<XmlElement>>> enumerator2 = null;
					faultAssertionsSelection = null;
				}
				IEnumerator<Dictionary<FaultDescription, IEnumerable<XmlElement>>> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x040041DB RID: 16859
			private BindingElementCollection bindingElements = new BindingElementCollection();

			// Token: 0x040041DC RID: 16860
			private readonly PolicyAssertionCollection endpointAssertions;

			// Token: 0x040041DD RID: 16861
			private readonly Dictionary<OperationDescription, PolicyAssertionCollection> operationBindingAssertions = new Dictionary<OperationDescription, PolicyAssertionCollection>();

			// Token: 0x040041DE RID: 16862
			private readonly Dictionary<MessageDescription, PolicyAssertionCollection> messageBindingAssertions = new Dictionary<MessageDescription, PolicyAssertionCollection>();

			// Token: 0x040041DF RID: 16863
			private readonly Dictionary<FaultDescription, PolicyAssertionCollection> faultBindingAssertions = new Dictionary<FaultDescription, PolicyAssertionCollection>();

			// Token: 0x02000F0C RID: 3852
			internal class MaxItemsEnumerable<T> : IEnumerable<T>, IEnumerable
			{
				// Token: 0x060085C4 RID: 34244 RVA: 0x001EFF19 File Offset: 0x001EE119
				public MaxItemsEnumerable(IEnumerable<T> inner, int maxItems)
				{
					this.inner = inner;
					this.maxItems = maxItems;
				}

				// Token: 0x060085C5 RID: 34245 RVA: 0x001EFF2F File Offset: 0x001EE12F
				public IEnumerator<T> GetEnumerator()
				{
					return new MetadataImporter.ImportedPolicyConversionContext.MaxItemsEnumerator<T>(this.inner.GetEnumerator(), this.maxItems);
				}

				// Token: 0x060085C6 RID: 34246 RVA: 0x001EFF47 File Offset: 0x001EE147
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x04004D82 RID: 19842
				private IEnumerable<T> inner;

				// Token: 0x04004D83 RID: 19843
				private int maxItems;
			}

			// Token: 0x02000F0D RID: 3853
			internal class MaxItemsEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
			{
				// Token: 0x060085C7 RID: 34247 RVA: 0x001EFF4F File Offset: 0x001EE14F
				public MaxItemsEnumerator(IEnumerator<T> inner, int maxItems)
				{
					this.maxItems = maxItems;
					this.currentItem = 0;
					this.inner = inner;
				}

				// Token: 0x17001D63 RID: 7523
				// (get) Token: 0x060085C8 RID: 34248 RVA: 0x001EFF6C File Offset: 0x001EE16C
				public T Current
				{
					get
					{
						return this.inner.Current;
					}
				}

				// Token: 0x060085C9 RID: 34249 RVA: 0x001EFF79 File Offset: 0x001EE179
				public void Dispose()
				{
					this.inner.Dispose();
				}

				// Token: 0x17001D64 RID: 7524
				// (get) Token: 0x060085CA RID: 34250 RVA: 0x001EFF86 File Offset: 0x001EE186
				object IEnumerator.Current
				{
					get
					{
						return this.inner.Current;
					}
				}

				// Token: 0x060085CB RID: 34251 RVA: 0x001EFF94 File Offset: 0x001EE194
				public bool MoveNext()
				{
					bool result = this.inner.MoveNext();
					int num = this.currentItem + 1;
					this.currentItem = num;
					if (num > this.maxItems)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MetadataImporter.ImportedPolicyConversionContext.MaxItemsEnumeratorExceededMaxItemsException());
					}
					return result;
				}

				// Token: 0x060085CC RID: 34252 RVA: 0x001EFFD7 File Offset: 0x001EE1D7
				public void Reset()
				{
					this.currentItem = 0;
					this.inner.Reset();
				}

				// Token: 0x04004D84 RID: 19844
				private int maxItems;

				// Token: 0x04004D85 RID: 19845
				private int currentItem;

				// Token: 0x04004D86 RID: 19846
				private IEnumerator<T> inner;
			}

			// Token: 0x02000F0E RID: 3854
			internal class MaxItemsEnumeratorExceededMaxItemsException : Exception
			{
			}

			// Token: 0x02000F0F RID: 3855
			private static class PolicyIterationHelper
			{
				// Token: 0x060085CE RID: 34254 RVA: 0x001EFFF3 File Offset: 0x001EE1F3
				internal static IEnumerable<Dictionary<K, V>> GetCartesianProduct<K, V>(Dictionary<K, IEnumerable<V>> sets)
				{
					Dictionary<K, V> counterValue = new Dictionary<K, V>(sets.Count);
					KeyValuePair<K, IEnumerator<V>>[] digits = MetadataImporter.ImportedPolicyConversionContext.PolicyIterationHelper.InitializeCounter<K, V>(sets, counterValue);
					do
					{
						yield return counterValue;
					}
					while (MetadataImporter.ImportedPolicyConversionContext.PolicyIterationHelper.IncrementCounter<K, V>(digits, sets, counterValue));
					yield break;
				}

				// Token: 0x060085CF RID: 34255 RVA: 0x001F0004 File Offset: 0x001EE204
				private static KeyValuePair<K, IEnumerator<V>>[] InitializeCounter<K, V>(Dictionary<K, IEnumerable<V>> sets, Dictionary<K, V> counterValue)
				{
					KeyValuePair<K, IEnumerator<V>>[] array = new KeyValuePair<K, IEnumerator<V>>[sets.Count];
					int num = 0;
					foreach (KeyValuePair<K, IEnumerable<V>> keyValuePair in sets)
					{
						array[num] = new KeyValuePair<K, IEnumerator<V>>(keyValuePair.Key, keyValuePair.Value.GetEnumerator());
						if (!array[num].Value.MoveNext())
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Each set must have at least one item in it", new object[0])));
						}
						counterValue[array[num].Key] = array[num].Value.Current;
						num++;
					}
					return array;
				}

				// Token: 0x060085D0 RID: 34256 RVA: 0x001F00E0 File Offset: 0x001EE2E0
				private static bool IncrementCounter<K, V>(KeyValuePair<K, IEnumerator<V>>[] digits, Dictionary<K, IEnumerable<V>> sets, Dictionary<K, V> counterValue)
				{
					int num = 0;
					while (num < digits.Length && !digits[num].Value.MoveNext())
					{
						IEnumerator<V> enumerator = sets[digits[num].Key].GetEnumerator();
						digits[num] = new KeyValuePair<K, IEnumerator<V>>(digits[num].Key, enumerator);
						digits[num].Value.MoveNext();
						num++;
					}
					if (num == digits.Length)
					{
						return false;
					}
					for (int i = num; i >= 0; i--)
					{
						counterValue[digits[i].Key] = digits[i].Value.Current;
					}
					return true;
				}
			}
		}

		// Token: 0x02000BB9 RID: 3001
		internal class PolicyAlternatives
		{
			// Token: 0x040041E0 RID: 16864
			public IEnumerable<IEnumerable<XmlElement>> EndpointAlternatives;

			// Token: 0x040041E1 RID: 16865
			public Dictionary<OperationDescription, IEnumerable<IEnumerable<XmlElement>>> OperationBindingAlternatives;

			// Token: 0x040041E2 RID: 16866
			public Dictionary<MessageDescription, IEnumerable<IEnumerable<XmlElement>>> MessageBindingAlternatives;

			// Token: 0x040041E3 RID: 16867
			public Dictionary<FaultDescription, IEnumerable<IEnumerable<XmlElement>>> FaultBindingAlternatives;
		}

		// Token: 0x02000BBA RID: 3002
		internal class BindingOnlyPolicyConversionContext : PolicyConversionContext
		{
			// Token: 0x06007458 RID: 29784 RVA: 0x001B260C File Offset: 0x001B080C
			internal BindingOnlyPolicyConversionContext(ServiceEndpoint endpoint, IEnumerable<XmlElement> bindingPolicy) : base(endpoint)
			{
				this.bindingPolicy = new PolicyAssertionCollection(bindingPolicy);
			}

			// Token: 0x17001AE6 RID: 6886
			// (get) Token: 0x06007459 RID: 29785 RVA: 0x001B262C File Offset: 0x001B082C
			public override BindingElementCollection BindingElements
			{
				get
				{
					return this.bindingElements;
				}
			}

			// Token: 0x0600745A RID: 29786 RVA: 0x001B2634 File Offset: 0x001B0834
			public override PolicyAssertionCollection GetBindingAssertions()
			{
				return this.bindingPolicy;
			}

			// Token: 0x0600745B RID: 29787 RVA: 0x001B263C File Offset: 0x001B083C
			public override PolicyAssertionCollection GetOperationBindingAssertions(OperationDescription operation)
			{
				return MetadataImporter.BindingOnlyPolicyConversionContext.noPolicy;
			}

			// Token: 0x0600745C RID: 29788 RVA: 0x001B2643 File Offset: 0x001B0843
			public override PolicyAssertionCollection GetMessageBindingAssertions(MessageDescription message)
			{
				return MetadataImporter.BindingOnlyPolicyConversionContext.noPolicy;
			}

			// Token: 0x0600745D RID: 29789 RVA: 0x001B264A File Offset: 0x001B084A
			public override PolicyAssertionCollection GetFaultBindingAssertions(FaultDescription fault)
			{
				return MetadataImporter.BindingOnlyPolicyConversionContext.noPolicy;
			}

			// Token: 0x040041E4 RID: 16868
			private static readonly PolicyAssertionCollection noPolicy = new PolicyAssertionCollection();

			// Token: 0x040041E5 RID: 16869
			private readonly BindingElementCollection bindingElements = new BindingElementCollection();

			// Token: 0x040041E6 RID: 16870
			private readonly PolicyAssertionCollection bindingPolicy;
		}

		// Token: 0x02000BBB RID: 3003
		// (Invoke) Token: 0x06007460 RID: 29792
		internal delegate void PolicyWarningHandler(XmlElement contextAssertion, string warningMessage);

		// Token: 0x02000BBC RID: 3004
		private sealed class PolicyReader
		{
			// Token: 0x06007463 RID: 29795 RVA: 0x001B265D File Offset: 0x001B085D
			internal PolicyReader(MetadataImporter metadataImporter)
			{
				this.metadataImporter = metadataImporter;
			}

			// Token: 0x06007464 RID: 29796 RVA: 0x001B266C File Offset: 0x001B086C
			private IEnumerable<IEnumerable<XmlElement>> ReadNode(XmlNode node, XmlElement contextAssertion, MetadataImporter.YieldLimiter yieldLimiter)
			{
				if (this.nodesRead >= this.metadataImporter.Quotas.MaxPolicyNodes)
				{
					if (this.nodesRead == this.metadataImporter.Quotas.MaxPolicyNodes)
					{
						string @string = SR.GetString("ExceededMaxPolicyComplexity", new object[]
						{
							node.Name,
							MetadataImporter.PolicyHelper.GetFragmentIdentifier((XmlElement)node)
						});
						this.metadataImporter.PolicyWarningOccured(contextAssertion, @string);
						this.nodesRead++;
					}
					return MetadataImporter.PolicyReader.EmptyEmpty;
				}
				this.nodesRead++;
				IEnumerable<IEnumerable<XmlElement>> result = MetadataImporter.PolicyReader.EmptyEmpty;
				switch (MetadataImporter.PolicyHelper.GetNodeType(node))
				{
				case MetadataImporter.PolicyHelper.NodeType.Policy:
				case MetadataImporter.PolicyHelper.NodeType.All:
					result = this.ReadNode_PolicyOrAll((XmlElement)node, contextAssertion, yieldLimiter);
					break;
				case MetadataImporter.PolicyHelper.NodeType.ExactlyOne:
					result = this.ReadNode_ExactlyOne((XmlElement)node, contextAssertion, yieldLimiter);
					break;
				case MetadataImporter.PolicyHelper.NodeType.Assertion:
					result = this.ReadNode_Assertion((XmlElement)node, yieldLimiter);
					break;
				case MetadataImporter.PolicyHelper.NodeType.PolicyReference:
					result = this.ReadNode_PolicyReference((XmlElement)node, contextAssertion, yieldLimiter);
					break;
				case MetadataImporter.PolicyHelper.NodeType.UnrecognizedWSPolicy:
				{
					string string2 = SR.GetString("UnrecognizedPolicyElementInNamespace", new object[]
					{
						node.Name,
						node.NamespaceURI
					});
					this.metadataImporter.PolicyWarningOccured(contextAssertion, string2);
					break;
				}
				}
				return result;
			}

			// Token: 0x06007465 RID: 29797 RVA: 0x001B27B0 File Offset: 0x001B09B0
			private IEnumerable<IEnumerable<XmlElement>> ReadNode_PolicyReference(XmlElement element, XmlElement contextAssertion, MetadataImporter.YieldLimiter yieldLimiter)
			{
				string attribute = element.GetAttribute("URI");
				if (attribute == null)
				{
					string @string = SR.GetString("PolicyReferenceMissingURI", new object[]
					{
						"URI"
					});
					this.metadataImporter.PolicyWarningOccured(contextAssertion, @string);
					return MetadataImporter.PolicyReader.EmptyEmpty;
				}
				if (attribute == string.Empty)
				{
					string string2 = SR.GetString("PolicyReferenceInvalidId");
					this.metadataImporter.PolicyWarningOccured(contextAssertion, string2);
					return MetadataImporter.PolicyReader.EmptyEmpty;
				}
				XmlElement xmlElement = this.metadataImporter.ResolvePolicyReference(attribute, contextAssertion);
				if (xmlElement == null)
				{
					string string3 = SR.GetString("UnableToFindPolicyWithId", new object[]
					{
						attribute
					});
					this.metadataImporter.PolicyWarningOccured(contextAssertion, string3);
					return MetadataImporter.PolicyReader.EmptyEmpty;
				}
				return this.ReadNode_PolicyOrAll(xmlElement, xmlElement, yieldLimiter);
			}

			// Token: 0x06007466 RID: 29798 RVA: 0x001B2876 File Offset: 0x001B0A76
			private IEnumerable<IEnumerable<XmlElement>> ReadNode_Assertion(XmlElement element, MetadataImporter.YieldLimiter yieldLimiter)
			{
				if (yieldLimiter.IncrementAndLogIfExceededLimit())
				{
					yield return MetadataImporter.PolicyReader.Empty;
				}
				else
				{
					yield return new MetadataImporter.PolicyHelper.SingleEnumerable<XmlElement>(element);
				}
				yield break;
			}

			// Token: 0x06007467 RID: 29799 RVA: 0x001B288D File Offset: 0x001B0A8D
			private IEnumerable<IEnumerable<XmlElement>> ReadNode_ExactlyOne(XmlElement element, XmlElement contextAssertion, MetadataImporter.YieldLimiter yieldLimiter)
			{
				foreach (object obj in element.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						foreach (IEnumerable<XmlElement> enumerable in this.ReadNode(xmlNode, contextAssertion, yieldLimiter))
						{
							if (yieldLimiter.IncrementAndLogIfExceededLimit())
							{
								yield break;
							}
							yield return enumerable;
						}
						IEnumerator<IEnumerable<XmlElement>> enumerator2 = null;
					}
				}
				IEnumerator enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06007468 RID: 29800 RVA: 0x001B28B4 File Offset: 0x001B0AB4
			private IEnumerable<IEnumerable<XmlElement>> ReadNode_PolicyOrAll(XmlElement element, XmlElement contextAssertion, MetadataImporter.YieldLimiter yieldLimiter)
			{
				IEnumerable<IEnumerable<XmlElement>> enumerable = MetadataImporter.PolicyReader.EmptyEmpty;
				foreach (object obj in element.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						IEnumerable<IEnumerable<XmlElement>> ys = this.ReadNode(xmlNode, contextAssertion, yieldLimiter);
						enumerable = MetadataImporter.PolicyHelper.CrossProduct<XmlElement>(enumerable, ys, yieldLimiter);
					}
				}
				return enumerable;
			}

			// Token: 0x06007469 RID: 29801 RVA: 0x001B292C File Offset: 0x001B0B2C
			internal IEnumerable<IEnumerable<XmlElement>> NormalizePolicy(IEnumerable<XmlElement> policyAssertions)
			{
				IEnumerable<IEnumerable<XmlElement>> enumerable = MetadataImporter.PolicyReader.EmptyEmpty;
				MetadataImporter.YieldLimiter yieldLimiter = new MetadataImporter.YieldLimiter(this.metadataImporter.Quotas.MaxYields, this.metadataImporter);
				foreach (XmlElement xmlElement in policyAssertions)
				{
					IEnumerable<IEnumerable<XmlElement>> ys = this.ReadNode(xmlElement, xmlElement, yieldLimiter);
					enumerable = MetadataImporter.PolicyHelper.CrossProduct<XmlElement>(enumerable, ys, yieldLimiter);
				}
				return enumerable;
			}

			// Token: 0x040041E7 RID: 16871
			private int nodesRead;

			// Token: 0x040041E8 RID: 16872
			private readonly MetadataImporter metadataImporter;

			// Token: 0x040041E9 RID: 16873
			private static IEnumerable<XmlElement> Empty = new MetadataImporter.PolicyHelper.EmptyEnumerable<XmlElement>();

			// Token: 0x040041EA RID: 16874
			private static IEnumerable<IEnumerable<XmlElement>> EmptyEmpty = new MetadataImporter.PolicyHelper.SingleEnumerable<IEnumerable<XmlElement>>(new MetadataImporter.PolicyHelper.EmptyEnumerable<XmlElement>());
		}

		// Token: 0x02000BBD RID: 3005
		internal class YieldLimiter
		{
			// Token: 0x0600746B RID: 29803 RVA: 0x001B29C3 File Offset: 0x001B0BC3
			internal YieldLimiter(int maxYields, MetadataImporter metadataImporter)
			{
				this.metadataImporter = metadataImporter;
				this.yieldsHit = 0;
				this.maxYields = maxYields;
			}

			// Token: 0x0600746C RID: 29804 RVA: 0x001B29E0 File Offset: 0x001B0BE0
			internal bool IncrementAndLogIfExceededLimit()
			{
				int num = this.yieldsHit + 1;
				this.yieldsHit = num;
				if (num > this.maxYields)
				{
					string @string = SR.GetString("ExceededMaxPolicySize");
					this.metadataImporter.PolicyWarningOccured(null, @string);
					return true;
				}
				return false;
			}

			// Token: 0x040041EB RID: 16875
			private int maxYields;

			// Token: 0x040041EC RID: 16876
			private int yieldsHit;

			// Token: 0x040041ED RID: 16877
			private readonly MetadataImporter metadataImporter;
		}

		// Token: 0x02000BBE RID: 3006
		internal static class PolicyHelper
		{
			// Token: 0x0600746D RID: 29805 RVA: 0x001B2A28 File Offset: 0x001B0C28
			internal static string GetFragmentIdentifier(XmlElement element)
			{
				string attribute = element.GetAttribute("Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
				if (attribute == null)
				{
					attribute = element.GetAttribute("id", "http://www.w3.org/XML/1998/namespace");
				}
				if (string.IsNullOrEmpty(attribute))
				{
					return string.Empty;
				}
				return string.Format(CultureInfo.InvariantCulture, "#{0}", new object[]
				{
					attribute
				});
			}

			// Token: 0x0600746E RID: 29806 RVA: 0x001B2A81 File Offset: 0x001B0C81
			internal static bool IsPolicyURIs(XmlAttribute attribute)
			{
				return (attribute.NamespaceURI == "http://schemas.xmlsoap.org/ws/2004/09/policy" || attribute.NamespaceURI == "http://www.w3.org/ns/ws-policy") && attribute.LocalName == "PolicyURIs";
			}

			// Token: 0x0600746F RID: 29807 RVA: 0x001B2ABC File Offset: 0x001B0CBC
			internal static MetadataImporter.PolicyHelper.NodeType GetNodeType(XmlNode node)
			{
				XmlElement xmlElement = node as XmlElement;
				if (xmlElement == null)
				{
					return MetadataImporter.PolicyHelper.NodeType.NonElement;
				}
				if (xmlElement.NamespaceURI != "http://schemas.xmlsoap.org/ws/2004/09/policy" && xmlElement.NamespaceURI != "http://www.w3.org/ns/ws-policy")
				{
					return MetadataImporter.PolicyHelper.NodeType.Assertion;
				}
				if (xmlElement.LocalName == "Policy")
				{
					return MetadataImporter.PolicyHelper.NodeType.Policy;
				}
				if (xmlElement.LocalName == "All")
				{
					return MetadataImporter.PolicyHelper.NodeType.All;
				}
				if (xmlElement.LocalName == "ExactlyOne")
				{
					return MetadataImporter.PolicyHelper.NodeType.ExactlyOne;
				}
				if (xmlElement.LocalName == "PolicyReference")
				{
					return MetadataImporter.PolicyHelper.NodeType.PolicyReference;
				}
				return MetadataImporter.PolicyHelper.NodeType.UnrecognizedWSPolicy;
			}

			// Token: 0x06007470 RID: 29808 RVA: 0x001B2B4C File Offset: 0x001B0D4C
			internal static IEnumerable<IEnumerable<T>> CrossProduct<T>(IEnumerable<IEnumerable<T>> xs, IEnumerable<IEnumerable<T>> ys, MetadataImporter.YieldLimiter yieldLimiter)
			{
				foreach (IEnumerable<T> x in MetadataImporter.PolicyHelper.AtLeastOne<T>(xs, yieldLimiter))
				{
					foreach (IEnumerable<T> e in MetadataImporter.PolicyHelper.AtLeastOne<T>(ys, yieldLimiter))
					{
						if (yieldLimiter.IncrementAndLogIfExceededLimit())
						{
							yield break;
						}
						yield return MetadataImporter.PolicyHelper.Merge<T>(x, e, yieldLimiter);
					}
					IEnumerator<IEnumerable<T>> enumerator2 = null;
					x = null;
				}
				IEnumerator<IEnumerable<T>> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06007471 RID: 29809 RVA: 0x001B2B6A File Offset: 0x001B0D6A
			private static IEnumerable<IEnumerable<T>> AtLeastOne<T>(IEnumerable<IEnumerable<T>> xs, MetadataImporter.YieldLimiter yieldLimiter)
			{
				bool gotOne = false;
				foreach (IEnumerable<T> enumerable in xs)
				{
					gotOne = true;
					if (yieldLimiter.IncrementAndLogIfExceededLimit())
					{
						yield break;
					}
					yield return enumerable;
				}
				IEnumerator<IEnumerable<T>> enumerator = null;
				if (!gotOne)
				{
					if (yieldLimiter.IncrementAndLogIfExceededLimit())
					{
						yield break;
					}
					yield return new MetadataImporter.PolicyHelper.EmptyEnumerable<T>();
				}
				yield break;
				yield break;
			}

			// Token: 0x06007472 RID: 29810 RVA: 0x001B2B81 File Offset: 0x001B0D81
			private static IEnumerable<T> Merge<T>(IEnumerable<T> e1, IEnumerable<T> e2, MetadataImporter.YieldLimiter yieldLimiter)
			{
				foreach (T t in e1)
				{
					if (yieldLimiter.IncrementAndLogIfExceededLimit())
					{
						yield break;
					}
					yield return t;
				}
				IEnumerator<T> enumerator = null;
				foreach (T t2 in e2)
				{
					if (yieldLimiter.IncrementAndLogIfExceededLimit())
					{
						yield break;
					}
					yield return t2;
				}
				enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x02000F13 RID: 3859
			internal class EmptyEnumerable<T> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IDisposable, IEnumerator
			{
				// Token: 0x060085EF RID: 34287 RVA: 0x001F091B File Offset: 0x001EEB1B
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060085F0 RID: 34288 RVA: 0x001F0923 File Offset: 0x001EEB23
				public IEnumerator<T> GetEnumerator()
				{
					return this;
				}

				// Token: 0x17001D6B RID: 7531
				// (get) Token: 0x060085F1 RID: 34289 RVA: 0x001F0926 File Offset: 0x001EEB26
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x17001D6C RID: 7532
				// (get) Token: 0x060085F2 RID: 34290 RVA: 0x001F0933 File Offset: 0x001EEB33
				public T Current
				{
					get
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoValue0")));
					}
				}

				// Token: 0x060085F3 RID: 34291 RVA: 0x001F094E File Offset: 0x001EEB4E
				public bool MoveNext()
				{
					return false;
				}

				// Token: 0x060085F4 RID: 34292 RVA: 0x001F0951 File Offset: 0x001EEB51
				public void Dispose()
				{
				}

				// Token: 0x060085F5 RID: 34293 RVA: 0x001F0953 File Offset: 0x001EEB53
				void IEnumerator.Reset()
				{
				}
			}

			// Token: 0x02000F14 RID: 3860
			internal class SingleEnumerable<T> : IEnumerable<!0>, IEnumerable
			{
				// Token: 0x060085F7 RID: 34295 RVA: 0x001F095D File Offset: 0x001EEB5D
				internal SingleEnumerable(T value)
				{
					this.value = value;
				}

				// Token: 0x060085F8 RID: 34296 RVA: 0x001F096C File Offset: 0x001EEB6C
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060085F9 RID: 34297 RVA: 0x001F0974 File Offset: 0x001EEB74
				public IEnumerator<T> GetEnumerator()
				{
					yield return this.value;
					yield break;
				}

				// Token: 0x04004DAC RID: 19884
				private T value;
			}

			// Token: 0x02000F15 RID: 3861
			internal enum NodeType
			{
				// Token: 0x04004DAE RID: 19886
				NonElement,
				// Token: 0x04004DAF RID: 19887
				Policy,
				// Token: 0x04004DB0 RID: 19888
				All,
				// Token: 0x04004DB1 RID: 19889
				ExactlyOne,
				// Token: 0x04004DB2 RID: 19890
				Assertion,
				// Token: 0x04004DB3 RID: 19891
				PolicyReference,
				// Token: 0x04004DB4 RID: 19892
				UnrecognizedWSPolicy
			}
		}
	}
}
