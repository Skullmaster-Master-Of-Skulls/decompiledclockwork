using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000584 RID: 1412
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true, "Transformers")]
	public sealed class WebPartConnection
	{
		// Token: 0x06004785 RID: 18309 RVA: 0x000EBDBC File Offset: 0x000E9FBC
		public WebPartConnection()
		{
			this._isStatic = true;
			this._isShared = true;
		}

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x06004786 RID: 18310 RVA: 0x000EBDD4 File Offset: 0x000E9FD4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebPart Consumer
		{
			get
			{
				string consumerID = this.ConsumerID;
				if (consumerID.Length == 0)
				{
					throw new InvalidOperationException(SR.GetString("WebPartConnection_ConsumerIDNotSet"));
				}
				if (this._webPartManager != null)
				{
					return this._webPartManager.WebParts[consumerID];
				}
				return null;
			}
		}

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x06004787 RID: 18311 RVA: 0x000EBE1C File Offset: 0x000EA01C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ConsumerConnectionPoint ConsumerConnectionPoint
		{
			get
			{
				WebPart consumer = this.Consumer;
				if (consumer != null && this._webPartManager != null)
				{
					return this._webPartManager.GetConsumerConnectionPoint(consumer, this.ConsumerConnectionPointID);
				}
				return null;
			}
		}

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x06004788 RID: 18312 RVA: 0x000EBE4F File Offset: 0x000EA04F
		// (set) Token: 0x06004789 RID: 18313 RVA: 0x000EBE6A File Offset: 0x000EA06A
		[DefaultValue("default")]
		public string ConsumerConnectionPointID
		{
			get
			{
				if (string.IsNullOrEmpty(this._consumerConnectionPointID))
				{
					return ConnectionPoint.DefaultID;
				}
				return this._consumerConnectionPointID;
			}
			set
			{
				this._consumerConnectionPointID = value;
			}
		}

		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x0600478A RID: 18314 RVA: 0x000EBE73 File Offset: 0x000EA073
		// (set) Token: 0x0600478B RID: 18315 RVA: 0x000EBE89 File Offset: 0x000EA089
		[DefaultValue("")]
		public string ConsumerID
		{
			get
			{
				if (this._consumerID == null)
				{
					return string.Empty;
				}
				return this._consumerID;
			}
			set
			{
				this._consumerID = value;
			}
		}

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x0600478C RID: 18316 RVA: 0x000EBE92 File Offset: 0x000EA092
		// (set) Token: 0x0600478D RID: 18317 RVA: 0x000EBE9A File Offset: 0x000EA09A
		internal bool Deleted
		{
			get
			{
				return this._deleted;
			}
			set
			{
				this._deleted = value;
			}
		}

		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x0600478E RID: 18318 RVA: 0x000EBEA3 File Offset: 0x000EA0A3
		// (set) Token: 0x0600478F RID: 18319 RVA: 0x000EBEB9 File Offset: 0x000EA0B9
		[DefaultValue("")]
		public string ID
		{
			get
			{
				if (this._id == null)
				{
					return string.Empty;
				}
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x06004790 RID: 18320 RVA: 0x000EBEC2 File Offset: 0x000EA0C2
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
		}

		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x06004791 RID: 18321 RVA: 0x000EBECA File Offset: 0x000EA0CA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsShared
		{
			get
			{
				return this._isShared;
			}
		}

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x06004792 RID: 18322 RVA: 0x000EBED2 File Offset: 0x000EA0D2
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsStatic
		{
			get
			{
				return this._isStatic;
			}
		}

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x06004793 RID: 18323 RVA: 0x000EBEDC File Offset: 0x000EA0DC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebPart Provider
		{
			get
			{
				string providerID = this.ProviderID;
				if (providerID.Length == 0)
				{
					throw new InvalidOperationException(SR.GetString("WebPartConnection_ProviderIDNotSet"));
				}
				if (this._webPartManager != null)
				{
					return this._webPartManager.WebParts[providerID];
				}
				return null;
			}
		}

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x06004794 RID: 18324 RVA: 0x000EBF24 File Offset: 0x000EA124
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ProviderConnectionPoint ProviderConnectionPoint
		{
			get
			{
				WebPart provider = this.Provider;
				if (provider != null && this._webPartManager != null)
				{
					return this._webPartManager.GetProviderConnectionPoint(provider, this.ProviderConnectionPointID);
				}
				return null;
			}
		}

		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x06004795 RID: 18325 RVA: 0x000EBF57 File Offset: 0x000EA157
		// (set) Token: 0x06004796 RID: 18326 RVA: 0x000EBF72 File Offset: 0x000EA172
		[DefaultValue("default")]
		public string ProviderConnectionPointID
		{
			get
			{
				if (string.IsNullOrEmpty(this._providerConnectionPointID))
				{
					return ConnectionPoint.DefaultID;
				}
				return this._providerConnectionPointID;
			}
			set
			{
				this._providerConnectionPointID = value;
			}
		}

		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x06004797 RID: 18327 RVA: 0x000EBF7B File Offset: 0x000EA17B
		// (set) Token: 0x06004798 RID: 18328 RVA: 0x000EBF91 File Offset: 0x000EA191
		[DefaultValue("")]
		public string ProviderID
		{
			get
			{
				if (this._providerID == null)
				{
					return string.Empty;
				}
				return this._providerID;
			}
			set
			{
				this._providerID = value;
			}
		}

		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x06004799 RID: 18329 RVA: 0x000EBF9A File Offset: 0x000EA19A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebPartTransformer Transformer
		{
			get
			{
				if (this._transformers == null || this._transformers.Count == 0)
				{
					return null;
				}
				return this._transformers[0];
			}
		}

		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x0600479A RID: 18330 RVA: 0x000EBFBF File Offset: 0x000EA1BF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public WebPartTransformerCollection Transformers
		{
			get
			{
				if (this._transformers == null)
				{
					this._transformers = new WebPartTransformerCollection();
				}
				return this._transformers;
			}
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x000EBFDC File Offset: 0x000EA1DC
		internal void Activate()
		{
			this.Transformers.SetReadOnly();
			WebPart provider = this.Provider;
			WebPart consumer = this.Consumer;
			Control control = provider.ToControl();
			Control control2 = consumer.ToControl();
			ProviderConnectionPoint providerConnectionPoint = this.ProviderConnectionPoint;
			if (!providerConnectionPoint.GetEnabled(control))
			{
				consumer.SetConnectErrorMessage(SR.GetString("WebPartConnection_DisabledConnectionPoint", new object[]
				{
					providerConnectionPoint.DisplayName,
					provider.DisplayTitle
				}));
				return;
			}
			ConsumerConnectionPoint consumerConnectionPoint = this.ConsumerConnectionPoint;
			if (!consumerConnectionPoint.GetEnabled(control2))
			{
				consumer.SetConnectErrorMessage(SR.GetString("WebPartConnection_DisabledConnectionPoint", new object[]
				{
					consumerConnectionPoint.DisplayName,
					consumer.DisplayTitle
				}));
				return;
			}
			if (!provider.IsClosed && !consumer.IsClosed)
			{
				WebPartTransformer transformer = this.Transformer;
				if (transformer == null)
				{
					object[] args;
					if (!(providerConnectionPoint.InterfaceType == consumerConnectionPoint.InterfaceType))
					{
						WebPart webPart = consumer;
						string name = "WebPartConnection_NoCommonInterface";
						args = new string[]
						{
							providerConnectionPoint.DisplayName,
							provider.DisplayTitle,
							consumerConnectionPoint.DisplayName,
							consumer.DisplayTitle
						};
						webPart.SetConnectErrorMessage(SR.GetString(name, args));
						return;
					}
					ConnectionInterfaceCollection secondaryInterfaces = providerConnectionPoint.GetSecondaryInterfaces(control);
					if (consumerConnectionPoint.SupportsConnection(control2, secondaryInterfaces))
					{
						object @object = providerConnectionPoint.GetObject(control);
						consumerConnectionPoint.SetObject(control2, @object);
						this._isActive = true;
						return;
					}
					WebPart webPart2 = consumer;
					string name2 = "WebPartConnection_IncompatibleSecondaryInterfaces";
					args = new string[]
					{
						consumerConnectionPoint.DisplayName,
						consumer.DisplayTitle,
						providerConnectionPoint.DisplayName,
						provider.DisplayTitle
					};
					webPart2.SetConnectErrorMessage(SR.GetString(name2, args));
					return;
				}
				else
				{
					Type type = transformer.GetType();
					if (!this._webPartManager.AvailableTransformers.Contains(type))
					{
						string @string;
						if (this._webPartManager.Context != null && this._webPartManager.Context.IsCustomErrorEnabled)
						{
							@string = SR.GetString("WebPartConnection_TransformerNotAvailable");
						}
						else
						{
							@string = SR.GetString("WebPartConnection_TransformerNotAvailableWithType", new object[]
							{
								type.FullName
							});
						}
						consumer.SetConnectErrorMessage(@string);
					}
					Type consumerType = WebPartTransformerAttribute.GetConsumerType(type);
					Type providerType = WebPartTransformerAttribute.GetProviderType(type);
					if (providerConnectionPoint.InterfaceType == consumerType && providerType == consumerConnectionPoint.InterfaceType)
					{
						if (consumerConnectionPoint.SupportsConnection(control2, ConnectionInterfaceCollection.Empty))
						{
							object object2 = providerConnectionPoint.GetObject(control);
							object data = transformer.Transform(object2);
							consumerConnectionPoint.SetObject(control2, data);
							this._isActive = true;
							return;
						}
						consumer.SetConnectErrorMessage(SR.GetString("WebPartConnection_ConsumerRequiresSecondaryInterfaces", new object[]
						{
							consumerConnectionPoint.DisplayName,
							consumer.DisplayTitle
						}));
						return;
					}
					else
					{
						if (providerConnectionPoint.InterfaceType != consumerType)
						{
							string string2;
							if (this._webPartManager.Context != null && this._webPartManager.Context.IsCustomErrorEnabled)
							{
								string2 = SR.GetString("WebPartConnection_IncompatibleProviderTransformer", new object[]
								{
									providerConnectionPoint.DisplayName,
									provider.DisplayTitle
								});
							}
							else
							{
								string2 = SR.GetString("WebPartConnection_IncompatibleProviderTransformerWithType", new object[]
								{
									providerConnectionPoint.DisplayName,
									provider.DisplayTitle,
									type.FullName
								});
							}
							consumer.SetConnectErrorMessage(string2);
							return;
						}
						string string3;
						if (this._webPartManager.Context != null && this._webPartManager.Context.IsCustomErrorEnabled)
						{
							string3 = SR.GetString("WebPartConnection_IncompatibleConsumerTransformer", new object[]
							{
								consumerConnectionPoint.DisplayName,
								consumer.DisplayTitle
							});
						}
						else
						{
							string3 = SR.GetString("WebPartConnection_IncompatibleConsumerTransformerWithType", new object[]
							{
								type.FullName,
								consumerConnectionPoint.DisplayName,
								consumer.DisplayTitle
							});
						}
						consumer.SetConnectErrorMessage(string3);
					}
				}
			}
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x000EC381 File Offset: 0x000EA581
		internal bool ConflictsWith(WebPartConnection otherConnection)
		{
			return this.ConflictsWithConsumer(otherConnection) || this.ConflictsWithProvider(otherConnection);
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x000EC395 File Offset: 0x000EA595
		internal bool ConflictsWithConsumer(WebPartConnection otherConnection)
		{
			return !this.ConsumerConnectionPoint.AllowsMultipleConnections && this.Consumer == otherConnection.Consumer && this.ConsumerConnectionPoint == otherConnection.ConsumerConnectionPoint;
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x000EC3C2 File Offset: 0x000EA5C2
		internal bool ConflictsWithProvider(WebPartConnection otherConnection)
		{
			return !this.ProviderConnectionPoint.AllowsMultipleConnections && this.Provider == otherConnection.Provider && this.ProviderConnectionPoint == otherConnection.ProviderConnectionPoint;
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x000EC3EF File Offset: 0x000EA5EF
		internal void SetIsShared(bool isShared)
		{
			this._isShared = isShared;
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x000EC3F8 File Offset: 0x000EA5F8
		internal void SetIsStatic(bool isStatic)
		{
			this._isStatic = isStatic;
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x000EC401 File Offset: 0x000EA601
		internal void SetTransformer(WebPartTransformer transformer)
		{
			if (this.Transformers.Count == 0)
			{
				this.Transformers.Add(transformer);
				return;
			}
			this.Transformers[0] = transformer;
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x000EC42B File Offset: 0x000EA62B
		internal void SetWebPartManager(WebPartManager webPartManager)
		{
			this._webPartManager = webPartManager;
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x000A9C25 File Offset: 0x000A7E25
		public override string ToString()
		{
			return base.GetType().Name;
		}

		// Token: 0x040026FC RID: 9980
		private string _consumerConnectionPointID;

		// Token: 0x040026FD RID: 9981
		private string _consumerID;

		// Token: 0x040026FE RID: 9982
		private bool _deleted;

		// Token: 0x040026FF RID: 9983
		private string _id;

		// Token: 0x04002700 RID: 9984
		private bool _isActive;

		// Token: 0x04002701 RID: 9985
		private bool _isShared;

		// Token: 0x04002702 RID: 9986
		private bool _isStatic;

		// Token: 0x04002703 RID: 9987
		private string _providerConnectionPointID;

		// Token: 0x04002704 RID: 9988
		private string _providerID;

		// Token: 0x04002705 RID: 9989
		private WebPartTransformerCollection _transformers;

		// Token: 0x04002706 RID: 9990
		private WebPartManager _webPartManager;
	}
}
