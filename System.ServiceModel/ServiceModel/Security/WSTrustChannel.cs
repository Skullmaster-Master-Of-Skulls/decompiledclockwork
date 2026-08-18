using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x02000372 RID: 882
	public class WSTrustChannel : IWSTrustChannelContract, IWSTrustContract, IChannel, ICommunicationObject
	{
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x00077AE0 File Offset: 0x00075CE0
		// (set) Token: 0x06002054 RID: 8276 RVA: 0x00077AE8 File Offset: 0x00075CE8
		public IChannel Channel
		{
			get
			{
				return this._innerChannel;
			}
			protected set
			{
				this._innerChannel = value;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x00077AF1 File Offset: 0x00075CF1
		// (set) Token: 0x06002056 RID: 8278 RVA: 0x00077AF9 File Offset: 0x00075CF9
		public WSTrustChannelFactory ChannelFactory
		{
			get
			{
				return this._factory;
			}
			protected set
			{
				this._factory = value;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x00077B02 File Offset: 0x00075D02
		// (set) Token: 0x06002058 RID: 8280 RVA: 0x00077B0A File Offset: 0x00075D0A
		public IWSTrustChannelContract Contract
		{
			get
			{
				return this._innerContract;
			}
			protected set
			{
				this._innerContract = value;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x00077B13 File Offset: 0x00075D13
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x00077B1B File Offset: 0x00075D1B
		public TrustVersion TrustVersion
		{
			get
			{
				return this._trustVersion;
			}
			protected set
			{
				if (value != null && value != TrustVersion.WSTrust13)
				{
					TrustVersion wstrustFeb = TrustVersion.WSTrustFeb2005;
				}
				this._trustVersion = value;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x0600205B RID: 8283 RVA: 0x00077B37 File Offset: 0x00075D37
		// (set) Token: 0x0600205C RID: 8284 RVA: 0x00077B3F File Offset: 0x00075D3F
		public WSTrustSerializationContext WSTrustSerializationContext
		{
			get
			{
				return this._context;
			}
			protected set
			{
				this._context = value;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x00077B48 File Offset: 0x00075D48
		// (set) Token: 0x0600205E RID: 8286 RVA: 0x00077B50 File Offset: 0x00075D50
		public WSTrustRequestSerializer WSTrustRequestSerializer
		{
			get
			{
				return this._wsTrustRequestSerializer;
			}
			protected set
			{
				this._wsTrustRequestSerializer = value;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x00077B59 File Offset: 0x00075D59
		// (set) Token: 0x06002060 RID: 8288 RVA: 0x00077B61 File Offset: 0x00075D61
		public WSTrustResponseSerializer WSTrustResponseSerializer
		{
			get
			{
				return this._wsTrustResponseSerializer;
			}
			protected set
			{
				this._wsTrustResponseSerializer = value;
			}
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x00077B6C File Offset: 0x00075D6C
		public WSTrustChannel(WSTrustChannelFactory factory, IWSTrustChannelContract inner, TrustVersion trustVersion, WSTrustSerializationContext context, WSTrustRequestSerializer requestSerializer, WSTrustResponseSerializer responseSerializer)
		{
			if (factory == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("inner");
			}
			if (inner == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("inner");
			}
			if (context == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("context");
			}
			if (requestSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestSerializer");
			}
			if (responseSerializer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("responseSerializer");
			}
			if (trustVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("trustVersion");
			}
			this._innerChannel = (inner as IChannel);
			if (this._innerChannel == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3286"));
			}
			this._innerContract = inner;
			this._factory = factory;
			this._context = context;
			this._wsTrustRequestSerializer = requestSerializer;
			this._wsTrustResponseSerializer = responseSerializer;
			this._trustVersion = trustVersion;
			this._messageVersion = MessageVersion.Default;
			if (this._factory.Endpoint != null && this._factory.Endpoint.Binding != null && this._factory.Endpoint.Binding.MessageVersion != null)
			{
				this._messageVersion = this._factory.Endpoint.Binding.MessageVersion;
			}
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00077CA1 File Offset: 0x00075EA1
		protected virtual Message CreateRequest(RequestSecurityToken request, string requestType)
		{
			return Message.CreateMessage(this._messageVersion, WSTrustChannel.GetRequestAction(requestType, this.TrustVersion), new WSTrustRequestBodyWriter(request, this.WSTrustRequestSerializer, this.WSTrustSerializationContext));
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x00077CCC File Offset: 0x00075ECC
		protected virtual RequestSecurityTokenResponse ReadResponse(Message response)
		{
			if (response == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("response");
			}
			if (response.IsFault)
			{
				MessageFault messageFault = MessageFault.CreateFault(response, 20480);
				string action = null;
				if (response.Headers != null)
				{
					action = response.Headers.Action;
				}
				FaultException exception = FaultException.CreateFault(messageFault, action, new Type[0]);
				throw FxTrace.Exception.AsError(exception);
			}
			return this.WSTrustResponseSerializer.ReadXml(response.GetReaderAtBodyContents(), this.WSTrustSerializationContext);
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x00077D48 File Offset: 0x00075F48
		protected static string GetRequestAction(string requestType, TrustVersion trustVersion)
		{
			if (trustVersion != TrustVersion.WSTrust13 && trustVersion != TrustVersion.WSTrustFeb2005)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID3137", new object[]
				{
					trustVersion.ToString()
				})));
			}
			if (!(requestType == "http://schemas.microsoft.com/idfx/requesttype/cancel"))
			{
				if (!(requestType == "http://schemas.microsoft.com/idfx/requesttype/issue"))
				{
					if (!(requestType == "http://schemas.microsoft.com/idfx/requesttype/renew"))
					{
						if (!(requestType == "http://schemas.microsoft.com/idfx/requesttype/validate"))
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID3141", new object[]
							{
								requestType.ToString()
							})));
						}
						if (trustVersion != TrustVersion.WSTrustFeb2005)
						{
							return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Validate";
						}
						return "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Validate";
					}
					else
					{
						if (trustVersion != TrustVersion.WSTrustFeb2005)
						{
							return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Renew";
						}
						return "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Renew";
					}
				}
				else
				{
					if (trustVersion != TrustVersion.WSTrustFeb2005)
					{
						return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Issue";
					}
					return "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Issue";
				}
			}
			else
			{
				if (trustVersion != TrustVersion.WSTrustFeb2005)
				{
					return "http://docs.oasis-open.org/ws-sx/ws-trust/200512/RST/Cancel";
				}
				return "http://schemas.xmlsoap.org/ws/2005/02/trust/RST/Cancel";
			}
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x00077E3C File Offset: 0x0007603C
		public virtual SecurityToken GetTokenFromResponse(RequestSecurityToken request, RequestSecurityTokenResponse response)
		{
			if (response == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("response");
			}
			if (!response.IsFinal)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException(SR.GetString("ID3270")));
			}
			if (response.RequestedSecurityToken == null)
			{
				return null;
			}
			SecurityToken securityToken = response.RequestedSecurityToken.SecurityToken;
			if (securityToken != null)
			{
				return securityToken;
			}
			if (response.RequestedSecurityToken.SecurityTokenXml == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID3138")));
			}
			SecurityToken proofKey = WSTrustChannel.GetProofKey(request, response);
			DateTime? dateTime = null;
			DateTime? dateTime2 = null;
			if (response.Lifetime != null)
			{
				dateTime = response.Lifetime.Created;
				dateTime2 = response.Lifetime.Expires;
				if (dateTime == null)
				{
					dateTime = new DateTime?(DateTime.UtcNow);
				}
				if (dateTime2 == null)
				{
					dateTime2 = new DateTime?(DateTime.UtcNow.AddHours(10.0));
				}
			}
			else
			{
				dateTime = new DateTime?(DateTime.UtcNow);
				dateTime2 = new DateTime?(DateTime.UtcNow.AddHours(10.0));
			}
			return new GenericXmlSecurityToken(response.RequestedSecurityToken.SecurityTokenXml, proofKey, dateTime.Value, dateTime2.Value, response.RequestedAttachedReference, response.RequestedUnattachedReference, new ReadOnlyCollection<IAuthorizationPolicy>(new List<IAuthorizationPolicy>()));
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x00077F98 File Offset: 0x00076198
		internal static SecurityToken GetUseKeySecurityToken(UseKey useKey, string requestKeyType)
		{
			if (useKey != null && useKey.Token != null)
			{
				return useKey.Token;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID3190", new object[]
			{
				requestKeyType
			})));
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x00077FD0 File Offset: 0x000761D0
		internal static WSTrustChannel.ProofKeyType GetKeyType(string keyType)
		{
			if (keyType == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/SymmetricKey" || keyType == "http://schemas.xmlsoap.org/ws/2005/02/trust/SymmetricKey" || keyType == "http://schemas.microsoft.com/idfx/keytype/symmetric" || string.IsNullOrEmpty(keyType))
			{
				return WSTrustChannel.ProofKeyType.Symmetric;
			}
			if (keyType == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/PublicKey" || keyType == "http://schemas.xmlsoap.org/ws/2005/02/trust/PublicKey" || keyType == "http://schemas.microsoft.com/idfx/keytype/asymmetric")
			{
				return WSTrustChannel.ProofKeyType.Asymmetric;
			}
			if (keyType == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/Bearer" || keyType == "http://schemas.xmlsoap.org/ws/2005/05/identity/NoProofKey" || keyType == "http://schemas.microsoft.com/idfx/keytype/bearer")
			{
				return WSTrustChannel.ProofKeyType.Bearer;
			}
			return WSTrustChannel.ProofKeyType.Unknown;
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x00078061 File Offset: 0x00076261
		internal static bool IsPsha1(string algorithm)
		{
			return algorithm == "http://docs.oasis-open.org/ws-sx/ws-trust/200512/CK/PSHA1" || algorithm == "http://schemas.xmlsoap.org/ws/2005/02/trust/CK/PSHA1" || algorithm == "http://schemas.microsoft.com/idfx/computedkeyalgorithm/psha1";
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x0007808C File Offset: 0x0007628C
		internal static SecurityToken ComputeProofKey(RequestSecurityToken request, RequestSecurityTokenResponse response)
		{
			if (response.Entropy == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID3193")));
			}
			if (request.Entropy == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID3194")));
			}
			int keySizeInBits = request.KeySizeInBits ?? 256;
			if (response.KeySizeInBits != null)
			{
				keySizeInBits = response.KeySizeInBits.Value;
			}
			byte[] key = CryptoHelper.KeyGenerator.ComputeCombinedKey(request.Entropy.GetKeyBytes(), response.Entropy.GetKeyBytes(), keySizeInBits);
			return new BinarySecretSecurityToken(key);
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00078140 File Offset: 0x00076340
		internal static SecurityToken GetProofKey(RequestSecurityToken request, RequestSecurityTokenResponse response)
		{
			if (response.RequestedProofToken != null)
			{
				if (response.RequestedProofToken.ProtectedKey != null)
				{
					return new BinarySecretSecurityToken(response.RequestedProofToken.ProtectedKey.GetKeyBytes());
				}
				if (WSTrustChannel.IsPsha1(response.RequestedProofToken.ComputedKeyAlgorithm))
				{
					return WSTrustChannel.ComputeProofKey(request, response);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID3192", new object[]
				{
					response.RequestedProofToken.ComputedKeyAlgorithm
				})));
			}
			else
			{
				switch (WSTrustChannel.GetKeyType(request.KeyType))
				{
				case WSTrustChannel.ProofKeyType.Bearer:
					return null;
				case WSTrustChannel.ProofKeyType.Symmetric:
					if (response.Entropy != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID3191")));
					}
					if (request.Entropy != null)
					{
						return new BinarySecretSecurityToken(request.Entropy.GetKeyBytes());
					}
					return null;
				case WSTrustChannel.ProofKeyType.Asymmetric:
					return WSTrustChannel.GetUseKeySecurityToken(request.UseKey, request.KeyType);
				default:
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID3139", new object[]
					{
						request.KeyType
					})));
				}
			}
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0007825B File Offset: 0x0007645B
		public T GetProperty<T>() where T : class
		{
			return this.Channel.GetProperty<T>();
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x00078268 File Offset: 0x00076468
		public void Abort()
		{
			this.Channel.Abort();
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x00078275 File Offset: 0x00076475
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.Channel.BeginClose(timeout, callback, state);
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x00078285 File Offset: 0x00076485
		public IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			return this.Channel.BeginClose(callback, state);
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x00078294 File Offset: 0x00076494
		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.Channel.BeginOpen(timeout, callback, state);
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x000782A4 File Offset: 0x000764A4
		public IAsyncResult BeginOpen(AsyncCallback callback, object state)
		{
			return this.Channel.BeginOpen(callback, state);
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x000782B3 File Offset: 0x000764B3
		public void Close(TimeSpan timeout)
		{
			this.Channel.Close(timeout);
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000782C1 File Offset: 0x000764C1
		public void Close()
		{
			this.Channel.Close();
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06002073 RID: 8307 RVA: 0x000782CE File Offset: 0x000764CE
		// (remove) Token: 0x06002074 RID: 8308 RVA: 0x000782DC File Offset: 0x000764DC
		public event EventHandler Closed
		{
			add
			{
				this.Channel.Closed += value;
			}
			remove
			{
				this.Channel.Closed -= value;
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06002075 RID: 8309 RVA: 0x000782EA File Offset: 0x000764EA
		// (remove) Token: 0x06002076 RID: 8310 RVA: 0x000782F8 File Offset: 0x000764F8
		public event EventHandler Closing
		{
			add
			{
				this.Channel.Closing += value;
			}
			remove
			{
				this.Channel.Closing -= value;
			}
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x00078306 File Offset: 0x00076506
		public void EndClose(IAsyncResult result)
		{
			this.Channel.EndClose(result);
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x00078314 File Offset: 0x00076514
		public void EndOpen(IAsyncResult result)
		{
			this.Channel.EndOpen(result);
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06002079 RID: 8313 RVA: 0x00078322 File Offset: 0x00076522
		// (remove) Token: 0x0600207A RID: 8314 RVA: 0x00078330 File Offset: 0x00076530
		public event EventHandler Faulted
		{
			add
			{
				this.Channel.Faulted += value;
			}
			remove
			{
				this.Channel.Faulted -= value;
			}
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x0007833E File Offset: 0x0007653E
		public void Open(TimeSpan timeout)
		{
			this.Channel.Open(timeout);
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x0007834C File Offset: 0x0007654C
		public void Open()
		{
			this.Channel.Open();
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x0600207D RID: 8317 RVA: 0x00078359 File Offset: 0x00076559
		// (remove) Token: 0x0600207E RID: 8318 RVA: 0x00078367 File Offset: 0x00076567
		public event EventHandler Opened
		{
			add
			{
				this.Channel.Opened += value;
			}
			remove
			{
				this.Channel.Opened -= value;
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x0600207F RID: 8319 RVA: 0x00078375 File Offset: 0x00076575
		// (remove) Token: 0x06002080 RID: 8320 RVA: 0x00078383 File Offset: 0x00076583
		public event EventHandler Opening
		{
			add
			{
				this.Channel.Opening += value;
			}
			remove
			{
				this.Channel.Opening -= value;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x00078391 File Offset: 0x00076591
		public CommunicationState State
		{
			get
			{
				return this.Channel.State;
			}
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0007839E File Offset: 0x0007659E
		public virtual RequestSecurityTokenResponse Cancel(RequestSecurityToken rst)
		{
			return this.ReadResponse(this.Contract.Cancel(this.CreateRequest(rst, "http://schemas.microsoft.com/idfx/requesttype/cancel")));
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x000783C0 File Offset: 0x000765C0
		public virtual SecurityToken Issue(RequestSecurityToken rst)
		{
			RequestSecurityTokenResponse requestSecurityTokenResponse = null;
			return this.Issue(rst, out requestSecurityTokenResponse);
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x000783D8 File Offset: 0x000765D8
		public virtual SecurityToken Issue(RequestSecurityToken rst, out RequestSecurityTokenResponse rstr)
		{
			Message message = this.CreateRequest(rst, "http://schemas.microsoft.com/idfx/requesttype/issue");
			Message response = this.Contract.Issue(message);
			rstr = this.ReadResponse(response);
			return this.GetTokenFromResponse(rst, rstr);
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x00078411 File Offset: 0x00076611
		public virtual RequestSecurityTokenResponse Renew(RequestSecurityToken rst)
		{
			return this.ReadResponse(this.Contract.Renew(this.CreateRequest(rst, "http://schemas.microsoft.com/idfx/requesttype/renew")));
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x00078430 File Offset: 0x00076630
		public virtual RequestSecurityTokenResponse Validate(RequestSecurityToken rst)
		{
			return this.ReadResponse(this.Contract.Validate(this.CreateRequest(rst, "http://schemas.microsoft.com/idfx/requesttype/validate")));
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x00078450 File Offset: 0x00076650
		private IAsyncResult BeginOperation(WSTrustChannel.WSTrustChannelAsyncResult.Operations operation, string requestType, RequestSecurityToken rst, AsyncCallback callback, object state)
		{
			if (rst == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rst");
			}
			Message request = this.CreateRequest(rst, requestType);
			WSTrustSerializationContext wstrustSerializationContext = this.WSTrustSerializationContext;
			return new WSTrustChannel.WSTrustChannelAsyncResult(this, operation, rst, wstrustSerializationContext, request, callback, state);
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x00078490 File Offset: 0x00076690
		private RequestSecurityTokenResponse EndOperation(IAsyncResult result, out WSTrustChannel.WSTrustChannelAsyncResult tcar)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			tcar = (result as WSTrustChannel.WSTrustChannelAsyncResult);
			if (tcar == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID2004", new object[]
				{
					typeof(WSTrustChannel.WSTrustChannelAsyncResult),
					result.GetType()
				}));
			}
			Message response = WSTrustChannel.WSTrustChannelAsyncResult.End(result);
			return this.ReadResponse(response);
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x000784F6 File Offset: 0x000766F6
		public IAsyncResult BeginCancel(RequestSecurityToken rst, AsyncCallback callback, object state)
		{
			return this.BeginOperation(WSTrustChannel.WSTrustChannelAsyncResult.Operations.Cancel, "http://schemas.microsoft.com/idfx/requesttype/cancel", rst, callback, state);
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x00078508 File Offset: 0x00076708
		public void EndCancel(IAsyncResult result, out RequestSecurityTokenResponse rstr)
		{
			WSTrustChannel.WSTrustChannelAsyncResult wstrustChannelAsyncResult;
			rstr = this.EndOperation(result, out wstrustChannelAsyncResult);
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00078520 File Offset: 0x00076720
		public IAsyncResult BeginIssue(RequestSecurityToken rst, AsyncCallback callback, object asyncState)
		{
			return this.BeginOperation(WSTrustChannel.WSTrustChannelAsyncResult.Operations.Issue, "http://schemas.microsoft.com/idfx/requesttype/issue", rst, callback, asyncState);
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00078534 File Offset: 0x00076734
		public SecurityToken EndIssue(IAsyncResult result, out RequestSecurityTokenResponse rstr)
		{
			WSTrustChannel.WSTrustChannelAsyncResult wstrustChannelAsyncResult;
			rstr = this.EndOperation(result, out wstrustChannelAsyncResult);
			return this.GetTokenFromResponse(wstrustChannelAsyncResult.RequestSecurityToken, rstr);
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x0007855A File Offset: 0x0007675A
		public IAsyncResult BeginRenew(RequestSecurityToken rst, AsyncCallback callback, object state)
		{
			return this.BeginOperation(WSTrustChannel.WSTrustChannelAsyncResult.Operations.Renew, "http://schemas.microsoft.com/idfx/requesttype/renew", rst, callback, state);
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x0007856C File Offset: 0x0007676C
		public void EndRenew(IAsyncResult result, out RequestSecurityTokenResponse rstr)
		{
			WSTrustChannel.WSTrustChannelAsyncResult wstrustChannelAsyncResult;
			rstr = this.EndOperation(result, out wstrustChannelAsyncResult);
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00078584 File Offset: 0x00076784
		public IAsyncResult BeginValidate(RequestSecurityToken rst, AsyncCallback callback, object state)
		{
			return this.BeginOperation(WSTrustChannel.WSTrustChannelAsyncResult.Operations.Validate, "http://schemas.microsoft.com/idfx/requesttype/validate", rst, callback, state);
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x00078598 File Offset: 0x00076798
		public void EndValidate(IAsyncResult result, out RequestSecurityTokenResponse rstr)
		{
			WSTrustChannel.WSTrustChannelAsyncResult wstrustChannelAsyncResult;
			rstr = this.EndOperation(result, out wstrustChannelAsyncResult);
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000785B0 File Offset: 0x000767B0
		public Message Cancel(Message message)
		{
			return this.Contract.Cancel(message);
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000785BE File Offset: 0x000767BE
		public IAsyncResult BeginCancel(Message message, AsyncCallback callback, object asyncState)
		{
			return this.Contract.BeginCancel(message, callback, asyncState);
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000785CE File Offset: 0x000767CE
		public Message EndCancel(IAsyncResult asyncResult)
		{
			return this.Contract.EndCancel(asyncResult);
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000785DC File Offset: 0x000767DC
		public Message Issue(Message message)
		{
			return this.Contract.Issue(message);
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000785EA File Offset: 0x000767EA
		public IAsyncResult BeginIssue(Message message, AsyncCallback callback, object asyncState)
		{
			return this.Contract.BeginIssue(message, callback, asyncState);
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000785FA File Offset: 0x000767FA
		public Message EndIssue(IAsyncResult asyncResult)
		{
			return this.Contract.EndIssue(asyncResult);
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x00078608 File Offset: 0x00076808
		public Message Renew(Message message)
		{
			return this.Contract.Renew(message);
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x00078616 File Offset: 0x00076816
		public IAsyncResult BeginRenew(Message message, AsyncCallback callback, object asyncState)
		{
			return this.Contract.BeginRenew(message, callback, asyncState);
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x00078626 File Offset: 0x00076826
		public Message EndRenew(IAsyncResult asyncResult)
		{
			return this.Contract.EndRenew(asyncResult);
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x00078634 File Offset: 0x00076834
		public Message Validate(Message message)
		{
			return this.Contract.Validate(message);
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x00078642 File Offset: 0x00076842
		public IAsyncResult BeginValidate(Message message, AsyncCallback callback, object asyncState)
		{
			return this.Contract.BeginValidate(message, callback, asyncState);
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00078652 File Offset: 0x00076852
		public Message EndValidate(IAsyncResult asyncResult)
		{
			return this.Contract.EndValidate(asyncResult);
		}

		// Token: 0x04001F0E RID: 7950
		private const int DefaultKeySizeInBits = 256;

		// Token: 0x04001F0F RID: 7951
		private const int FaultMaxBufferSize = 20480;

		// Token: 0x04001F10 RID: 7952
		private WSTrustChannelFactory _factory;

		// Token: 0x04001F11 RID: 7953
		private IChannel _innerChannel;

		// Token: 0x04001F12 RID: 7954
		private IWSTrustChannelContract _innerContract;

		// Token: 0x04001F13 RID: 7955
		private MessageVersion _messageVersion;

		// Token: 0x04001F14 RID: 7956
		private TrustVersion _trustVersion;

		// Token: 0x04001F15 RID: 7957
		private WSTrustSerializationContext _context;

		// Token: 0x04001F16 RID: 7958
		private WSTrustRequestSerializer _wsTrustRequestSerializer;

		// Token: 0x04001F17 RID: 7959
		private WSTrustResponseSerializer _wsTrustResponseSerializer;

		// Token: 0x02000B93 RID: 2963
		internal class WSTrustChannelAsyncResult : AsyncResult
		{
			// Token: 0x0600734E RID: 29518 RVA: 0x001AE1E8 File Offset: 0x001AC3E8
			public WSTrustChannelAsyncResult(IWSTrustContract client, WSTrustChannel.WSTrustChannelAsyncResult.Operations operation, RequestSecurityToken rst, WSTrustSerializationContext serializationContext, Message request, AsyncCallback callback, object state) : base(callback, state)
			{
				this._client = client;
				this._rst = rst;
				this._serializationContext = serializationContext;
				this._operation = operation;
				switch (this._operation)
				{
				case WSTrustChannel.WSTrustChannelAsyncResult.Operations.Cancel:
					client.BeginCancel(request, new AsyncCallback(this.OnOperationCompleted), null);
					return;
				case WSTrustChannel.WSTrustChannelAsyncResult.Operations.Issue:
					client.BeginIssue(request, new AsyncCallback(this.OnOperationCompleted), null);
					return;
				case WSTrustChannel.WSTrustChannelAsyncResult.Operations.Renew:
					client.BeginRenew(request, new AsyncCallback(this.OnOperationCompleted), null);
					return;
				case WSTrustChannel.WSTrustChannelAsyncResult.Operations.Validate:
					client.BeginValidate(request, new AsyncCallback(this.OnOperationCompleted), null);
					return;
				default:
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3285", new object[]
					{
						Enum.GetName(typeof(WSTrustChannel.WSTrustChannelAsyncResult.Operations), this._operation)
					}));
				}
			}

			// Token: 0x17001AB7 RID: 6839
			// (get) Token: 0x0600734F RID: 29519 RVA: 0x001AE2C9 File Offset: 0x001AC4C9
			// (set) Token: 0x06007350 RID: 29520 RVA: 0x001AE2D1 File Offset: 0x001AC4D1
			public IWSTrustContract Client
			{
				get
				{
					return this._client;
				}
				set
				{
					this._client = value;
				}
			}

			// Token: 0x17001AB8 RID: 6840
			// (get) Token: 0x06007351 RID: 29521 RVA: 0x001AE2DA File Offset: 0x001AC4DA
			// (set) Token: 0x06007352 RID: 29522 RVA: 0x001AE2E2 File Offset: 0x001AC4E2
			public RequestSecurityToken RequestSecurityToken
			{
				get
				{
					return this._rst;
				}
				set
				{
					this._rst = value;
				}
			}

			// Token: 0x17001AB9 RID: 6841
			// (get) Token: 0x06007353 RID: 29523 RVA: 0x001AE2EB File Offset: 0x001AC4EB
			// (set) Token: 0x06007354 RID: 29524 RVA: 0x001AE2F3 File Offset: 0x001AC4F3
			public Message Response
			{
				get
				{
					return this._response;
				}
				set
				{
					this._response = value;
				}
			}

			// Token: 0x17001ABA RID: 6842
			// (get) Token: 0x06007355 RID: 29525 RVA: 0x001AE2FC File Offset: 0x001AC4FC
			// (set) Token: 0x06007356 RID: 29526 RVA: 0x001AE304 File Offset: 0x001AC504
			public WSTrustSerializationContext SerializationContext
			{
				get
				{
					return this._serializationContext;
				}
				set
				{
					this._serializationContext = value;
				}
			}

			// Token: 0x06007357 RID: 29527 RVA: 0x001AE310 File Offset: 0x001AC510
			public new static Message End(IAsyncResult iar)
			{
				AsyncResult.End(iar);
				WSTrustChannel.WSTrustChannelAsyncResult wstrustChannelAsyncResult = iar as WSTrustChannel.WSTrustChannelAsyncResult;
				if (wstrustChannelAsyncResult == null)
				{
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID2004", new object[]
					{
						typeof(WSTrustChannel.WSTrustChannelAsyncResult),
						iar.GetType()
					}));
				}
				return wstrustChannelAsyncResult.Response;
			}

			// Token: 0x06007358 RID: 29528 RVA: 0x001AE360 File Offset: 0x001AC560
			private void OnOperationCompleted(IAsyncResult iar)
			{
				try
				{
					this.Response = this.EndOperation(iar);
					base.Complete(iar.CompletedSynchronously);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					base.Complete(false, exception);
				}
			}

			// Token: 0x06007359 RID: 29529 RVA: 0x001AE3B0 File Offset: 0x001AC5B0
			private Message EndOperation(IAsyncResult iar)
			{
				switch (this._operation)
				{
				case WSTrustChannel.WSTrustChannelAsyncResult.Operations.Cancel:
					return this.Client.EndCancel(iar);
				case WSTrustChannel.WSTrustChannelAsyncResult.Operations.Issue:
					return this.Client.EndIssue(iar);
				case WSTrustChannel.WSTrustChannelAsyncResult.Operations.Renew:
					return this.Client.EndRenew(iar);
				case WSTrustChannel.WSTrustChannelAsyncResult.Operations.Validate:
					return this.Client.EndValidate(iar);
				default:
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID3285", new object[]
					{
						this._operation
					}));
				}
			}

			// Token: 0x04004123 RID: 16675
			private IWSTrustContract _client;

			// Token: 0x04004124 RID: 16676
			private RequestSecurityToken _rst;

			// Token: 0x04004125 RID: 16677
			private WSTrustSerializationContext _serializationContext;

			// Token: 0x04004126 RID: 16678
			private Message _response;

			// Token: 0x04004127 RID: 16679
			private WSTrustChannel.WSTrustChannelAsyncResult.Operations _operation;

			// Token: 0x02000EFD RID: 3837
			public enum Operations
			{
				// Token: 0x04004D44 RID: 19780
				Cancel,
				// Token: 0x04004D45 RID: 19781
				Issue,
				// Token: 0x04004D46 RID: 19782
				Renew,
				// Token: 0x04004D47 RID: 19783
				Validate
			}
		}

		// Token: 0x02000B94 RID: 2964
		internal enum ProofKeyType
		{
			// Token: 0x04004129 RID: 16681
			Unknown,
			// Token: 0x0400412A RID: 16682
			Bearer,
			// Token: 0x0400412B RID: 16683
			Symmetric,
			// Token: 0x0400412C RID: 16684
			Asymmetric
		}
	}
}
