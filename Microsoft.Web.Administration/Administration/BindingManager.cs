using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Web.Management.Utility;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000015 RID: 21
	internal sealed class BindingManager
	{
		// Token: 0x06000111 RID: 273 RVA: 0x00004CAE File Offset: 0x00003CAE
		internal BindingManager(ServerManager serverManager)
		{
			this._serverManager = serverManager;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00004CBD File Offset: 0x00003CBD
		private IList<BindingManager.BindingTransaction> BindingTransactionList
		{
			get
			{
				if (this._bindingTransactionList == null)
				{
					this._bindingTransactionList = new List<BindingManager.BindingTransaction>();
				}
				return this._bindingTransactionList;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004CD8 File Offset: 0x00003CD8
		public void AddNewBindingTransaction(Binding binding, byte[] certificateHash, string certificateStoreName)
		{
			BindingManager.BindingTransaction item = BindingManager.BindingTransaction.CreateAddBindingTransaction(binding, certificateHash, certificateStoreName);
			this.BindingTransactionList.Add(item);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004CFC File Offset: 0x00003CFC
		public void AddModifyBindingTransaction(Binding binding, string originalEndPointProtocol, IPEndPoint originalEndPoint, string modifiedEndPointProtocol, IPEndPoint modifiedEndPoint, byte[] certificateHash, string certificateStoreName)
		{
			this._serverManager.EnsureLocal();
			BindingManager.BindingTransaction item = BindingManager.BindingTransaction.CreateModifyBindingTransaction(binding, originalEndPoint, originalEndPointProtocol, modifiedEndPoint, modifiedEndPointProtocol, certificateHash, certificateStoreName);
			this.BindingTransactionList.Add(item);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004D34 File Offset: 0x00003D34
		public void AddModifyDSMapperPropertyTransaction(Binding binding, bool useDsMapper)
		{
			BindingManager.BindingTransaction item = BindingManager.BindingTransaction.CreateModifyUseDsMapperTransaction(binding, useDsMapper);
			this.BindingTransactionList.Add(item);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004D58 File Offset: 0x00003D58
		public void AddRemoveBindingTransaction(Binding binding)
		{
			BindingManager.BindingTransaction item = BindingManager.BindingTransaction.CreateDeleteBindingTransaction(binding);
			this.BindingTransactionList.Add(item);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004D78 File Offset: 0x00003D78
		internal void RemoveTransactionsFor(Binding binding)
		{
			for (int i = this.BindingTransactionList.Count - 1; i >= 0; i--)
			{
				BindingManager.BindingTransaction bindingTransaction = this.BindingTransactionList[i];
				if (bindingTransaction.Binding == binding)
				{
					this.BindingTransactionList.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004DC0 File Offset: 0x00003DC0
		public void Save()
		{
			try
			{
				for (int i = 0; i < this.BindingTransactionList.Count; i++)
				{
					BindingManager.BindingTransaction bindingTransaction = this.BindingTransactionList[i];
					bindingTransaction.Commit();
				}
			}
			finally
			{
				this._bindingTransactionList = null;
			}
		}

		// Token: 0x04000038 RID: 56
		private List<BindingManager.BindingTransaction> _bindingTransactionList;

		// Token: 0x04000039 RID: 57
		private ServerManager _serverManager;

		// Token: 0x02000016 RID: 22
		internal enum BindingTransactionType
		{
			// Token: 0x0400003B RID: 59
			Add,
			// Token: 0x0400003C RID: 60
			Delete,
			// Token: 0x0400003D RID: 61
			Modify,
			// Token: 0x0400003E RID: 62
			ModifyUseDsMapperProperty
		}

		// Token: 0x02000017 RID: 23
		private class BindingTransaction
		{
			// Token: 0x06000119 RID: 281 RVA: 0x00004E10 File Offset: 0x00003E10
			private BindingTransaction()
			{
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x0600011A RID: 282 RVA: 0x00004E18 File Offset: 0x00003E18
			internal Binding Binding
			{
				get
				{
					return this._binding;
				}
			}

			// Token: 0x0600011B RID: 283 RVA: 0x00004E20 File Offset: 0x00003E20
			public static BindingManager.BindingTransaction CreateModifyUseDsMapperTransaction(Binding binding, bool useDsMapper)
			{
				return new BindingManager.BindingTransaction
				{
					_transactionType = BindingManager.BindingTransactionType.ModifyUseDsMapperProperty,
					_binding = binding,
					_useDsMapper = useDsMapper
				};
			}

			// Token: 0x0600011C RID: 284 RVA: 0x00004E4C File Offset: 0x00003E4C
			internal static BindingManager.BindingTransaction CreateDeleteBindingTransaction(Binding binding)
			{
				return new BindingManager.BindingTransaction
				{
					_transactionType = BindingManager.BindingTransactionType.Delete,
					_binding = binding
				};
			}

			// Token: 0x0600011D RID: 285 RVA: 0x00004E70 File Offset: 0x00003E70
			internal static BindingManager.BindingTransaction CreateAddBindingTransaction(Binding binding, byte[] certificateHash, string certificateStoreName)
			{
				return new BindingManager.BindingTransaction
				{
					_transactionType = BindingManager.BindingTransactionType.Add,
					_binding = binding,
					_certificateHash = certificateHash,
					_certificateStoreName = certificateStoreName
				};
			}

			// Token: 0x0600011E RID: 286 RVA: 0x00004EA0 File Offset: 0x00003EA0
			internal static BindingManager.BindingTransaction CreateModifyBindingTransaction(Binding binding, IPEndPoint originalEndPoint, string originalEndPointProtocol, IPEndPoint newEndPoint, string modifiedEndPointProtocol, byte[] certificateHash, string certificateStoreName)
			{
				return new BindingManager.BindingTransaction
				{
					_transactionType = BindingManager.BindingTransactionType.Modify,
					_endPoint = originalEndPoint,
					_binding = binding,
					_endPointProtocol = originalEndPointProtocol,
					_newEndPointProtocol = modifiedEndPointProtocol,
					_newEndPoint = newEndPoint,
					_certificateHash = certificateHash,
					_certificateStoreName = certificateStoreName
				};
			}

			// Token: 0x0600011F RID: 287 RVA: 0x00004EF0 File Offset: 0x00003EF0
			internal void Commit()
			{
				switch (this._transactionType)
				{
				case BindingManager.BindingTransactionType.Add:
					this._binding.AddSslCertificate(this._certificateHash, this._certificateStoreName);
					return;
				case BindingManager.BindingTransactionType.Delete:
					this._binding.RemoveSslCertificate();
					break;
				case BindingManager.BindingTransactionType.Modify:
					if (string.Equals(this._endPointProtocol, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
					{
						HttpApiWrapper.DeleteSSLBinding(this._endPoint);
					}
					if (string.Equals(this._newEndPointProtocol, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase) && this._newEndPoint != null && this._certificateHash != null)
					{
						HttpApiWrapper.CreateSSLBinding(this._newEndPoint, this._certificateHash, this._certificateStoreName);
						return;
					}
					break;
				case BindingManager.BindingTransactionType.ModifyUseDsMapperProperty:
					this._binding.SetDsMapper(this._useDsMapper);
					return;
				default:
					return;
				}
			}

			// Token: 0x0400003F RID: 63
			private BindingManager.BindingTransactionType _transactionType;

			// Token: 0x04000040 RID: 64
			private IPEndPoint _endPoint;

			// Token: 0x04000041 RID: 65
			private string _endPointProtocol;

			// Token: 0x04000042 RID: 66
			private string _newEndPointProtocol;

			// Token: 0x04000043 RID: 67
			private IPEndPoint _newEndPoint;

			// Token: 0x04000044 RID: 68
			private byte[] _certificateHash;

			// Token: 0x04000045 RID: 69
			private string _certificateStoreName;

			// Token: 0x04000046 RID: 70
			private bool _useDsMapper;

			// Token: 0x04000047 RID: 71
			private Binding _binding;
		}
	}
}
