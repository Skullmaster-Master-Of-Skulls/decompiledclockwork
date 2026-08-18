using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.BinXml
{
	// Token: 0x02000016 RID: 22
	internal class ObxmlDTDInfo : DTDObject
	{
		// Token: 0x060000CE RID: 206 RVA: 0x000038F8 File Offset: 0x00001AF8
		internal ObxmlDTDInfo()
		{
			this.ClearStateObject();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003908 File Offset: 0x00001B08
		internal ObxmlDTDInfo(ObxmlDecodeState mDecodeState)
		{
			this.ClearStateObject();
			this.SetDecodeStateObject(mDecodeState);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003920 File Offset: 0x00001B20
		internal void SetDecodeStateObject(ObxmlDecodeState mDecodeState)
		{
			this.m_DecodeState = mDecodeState;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000392C File Offset: 0x00001B2C
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00003934 File Offset: 0x00001B34
		internal bool IsValid { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003940 File Offset: 0x00001B40
		internal bool IsEmpty
		{
			get
			{
				return base.ObjectName == null;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000394C File Offset: 0x00001B4C
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00003954 File Offset: 0x00001B54
		internal bool IsExternal { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003960 File Offset: 0x00001B60
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00003968 File Offset: 0x00001B68
		internal bool IsProcessingDTD { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003974 File Offset: 0x00001B74
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x0000397C File Offset: 0x00001B7C
		internal bool InCDATA { get; set; }

		// Token: 0x060000DA RID: 218 RVA: 0x00003988 File Offset: 0x00001B88
		internal bool ProcessAttributes()
		{
			return true;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000398C File Offset: 0x00001B8C
		internal void InilializeSubset()
		{
			if (!this.m_InitSubset)
			{
				this.m_InitSubset = true;
				this.m_DecodeState.m_CurrentInstruction.AppendString(ObxmlInstructionState.sXmlTagSquareOpen, true, false);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000039B8 File Offset: 0x00001BB8
		internal bool StartDTD()
		{
			try
			{
				this.IsValid = true;
				this.PrintAttributeDecl();
				this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(ObxmlInstructionState.sXmlDocType, true, false);
				this.m_DecodeState.m_CurrentInstruction.AppendString(base.ObjectName, false, false);
				if (!string.IsNullOrEmpty(base.PublicId))
				{
					this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(ObxmlInstructionState.sXmlPublic, true, true);
					this.m_DecodeState.m_CurrentInstruction.AppendQuoted(base.PublicId, true, false);
					this.m_DecodeState.m_CurrentInstruction.AppendQuoted(base.SystemId, false, false);
				}
				else if (!string.IsNullOrEmpty(base.SystemId))
				{
					this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(ObxmlInstructionState.sXmlSystem, true, true);
					this.m_DecodeState.m_CurrentInstruction.AppendQuoted(base.SystemId, false, false);
				}
				this.IsProcessingDTD = true;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
			return true;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003AC8 File Offset: 0x00001CC8
		internal bool EndDTD()
		{
			try
			{
				this.PrintAttributeDecl();
				if (this.m_InitSubset)
				{
					this.m_DecodeState.m_CurrentInstruction.AppendString(ObxmlInstructionState.sXmlTagSquareClose, false, false);
				}
				this.m_InitSubset = false;
				this.m_DecodeState.m_CurrentInstruction.AppendString(ObxmlInstructionState.sXmlTagClosingBracket, true, false);
				this.IsProcessingDTD = false;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
			return true;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003B48 File Offset: 0x00001D48
		internal bool StartEntity(string name)
		{
			this.PrintAttributeDecl();
			this.m_DecodeState.m_CurrentInstruction.AppendEntity(name, false, false);
			return true;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003B68 File Offset: 0x00001D68
		internal bool EndEntity(string name)
		{
			this.PrintAttributeDecl();
			return true;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003B74 File Offset: 0x00001D74
		internal bool StartCDATA()
		{
			this.InCDATA = true;
			return this.m_DecodeState.m_CurrentInstruction.StartCDATA();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003B90 File Offset: 0x00001D90
		internal bool EndCDATA()
		{
			bool result = this.m_DecodeState.m_CurrentInstruction.EndCDATA();
			this.InCDATA = false;
			return result;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003BB8 File Offset: 0x00001DB8
		internal void ElementDecl(DTDElementInfo dtdElement)
		{
			this.ElementDecl(dtdElement.ElementName, dtdElement.ContentSpec);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003BCC File Offset: 0x00001DCC
		internal void ElementDecl(string name, string model)
		{
			try
			{
				this.PrintAttributeDecl();
				this.InilializeSubset();
				this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(ObxmlInstructionState.sXmlElement, true, false);
				this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(name, true, false);
				this.m_DecodeState.m_CurrentInstruction.AppendString(model, false, false);
				this.m_DecodeState.m_CurrentInstruction.AppendString(ObxmlInstructionState.sXmlTagClosingBracket, true, false);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003C60 File Offset: 0x00001E60
		internal void AttributeDecl(DTDElementAttributeInfo attributeInfo)
		{
			if (attributeInfo != null && attributeInfo.ElementName != this.m_lastElementString)
			{
				this.PrintAttributeDecl();
			}
			this.m_AttributeList.Add(attributeInfo);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003C8C File Offset: 0x00001E8C
		internal void AttributeDecl(string elementName, string attributeName, string type, string mode, string value)
		{
			DTDElementAttributeInfo attributeInfo = new DTDElementAttributeInfo(elementName, attributeName, type, mode, value);
			this.AttributeDecl(attributeInfo);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003CB0 File Offset: 0x00001EB0
		internal void EntityDecl(DTDObject entity)
		{
			if (entity.IsParsedEntity)
			{
				this.InternalEntityDecl(entity.ObjectName, entity.ObjectValue);
				return;
			}
			if (entity.IsUnparsedEntity)
			{
				this.UnparsedEntityDecl(entity.ObjectName, entity.PublicId, entity.SystemId, entity.Note);
				return;
			}
			this.ExternalEntityDecl(entity.ObjectName, entity.PublicId, entity.SystemId);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003D18 File Offset: 0x00001F18
		internal void EntityReference(DTDObject entity)
		{
			if (this.m_DecodeState.TryAppendingBeginTagClosure(null, true, false))
			{
				this.m_DecodeState.GetLastNodeState().BeginTagClosed = true;
			}
			this.m_DecodeState.m_CurrentInstruction.AppendEntity(entity.ObjectName, false, false);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003D54 File Offset: 0x00001F54
		internal void InternalEntityDecl(string name, string value)
		{
			try
			{
				this.PrintAttributeDecl();
				this.InilializeSubset();
				this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(ObxmlInstructionState.sXmlEntity, true, false);
				if (name.StartsWith(ObxmlInstructionState.sXmlPercentage))
				{
					this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(name.Substring(1, name.Length - 1), ObxmlInstructionState.sXmlWhitespaceBlank, ObxmlInstructionState.sXmlPercentage);
				}
				else
				{
					this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(name, true, false);
				}
				this.m_DecodeState.m_CurrentInstruction.AppendQuoted(value, false, false);
				this.m_DecodeState.m_CurrentInstruction.AppendString(ObxmlInstructionState.sXmlTagClosingBracket, true, false);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003E24 File Offset: 0x00002024
		internal void ExternalEntityDecl(string name, string publicId, string systemId)
		{
			try
			{
				this.PrintAttributeDecl();
				this.InilializeSubset();
				this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(ObxmlInstructionState.sXmlEntity, true, false);
				if (name.StartsWith(ObxmlInstructionState.sXmlPercentage))
				{
					this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(name.Substring(1, name.Length - 1), ObxmlInstructionState.sXmlWhitespaceBlank, ObxmlInstructionState.sXmlPercentage);
				}
				else
				{
					this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(name, true, false);
				}
				if (!string.IsNullOrEmpty(publicId))
				{
					this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(ObxmlInstructionState.sXmlPublic, true, true);
					this.m_DecodeState.m_CurrentInstruction.AppendQuoted(publicId, true, false);
					this.m_DecodeState.m_CurrentInstruction.AppendQuoted(systemId, false, false);
				}
				else if (!string.IsNullOrEmpty(systemId))
				{
					this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(ObxmlInstructionState.sXmlSystem, true, true);
					this.m_DecodeState.m_CurrentInstruction.AppendQuoted(systemId, false, false);
				}
				this.m_DecodeState.m_CurrentInstruction.AppendString(ObxmlInstructionState.sXmlTagClosingBracket, true, false);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003F68 File Offset: 0x00002168
		internal void UnparsedEntityDecl(string name, string publicId, string systemId, string notationName)
		{
			try
			{
				this.PrintAttributeDecl();
				this.InilializeSubset();
				this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(ObxmlInstructionState.sXmlEntity, true, false);
				this.m_DecodeState.m_CurrentInstruction.AppendString(name, false, false);
				bool prefixSpace = false;
				if (!string.IsNullOrEmpty(publicId))
				{
					this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(ObxmlInstructionState.sXmlPublic, true, true);
					this.m_DecodeState.m_CurrentInstruction.AppendQuoted(publicId, true, false);
					this.m_DecodeState.m_CurrentInstruction.AppendQuoted(systemId, false, false);
					prefixSpace = true;
				}
				else if (!string.IsNullOrEmpty(systemId))
				{
					this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(ObxmlInstructionState.sXmlSystem, true, true);
					this.m_DecodeState.m_CurrentInstruction.AppendQuoted(systemId, false, false);
					prefixSpace = true;
				}
				this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(ObxmlInstructionState.sXmlNdata, true, prefixSpace);
				this.m_DecodeState.m_CurrentInstruction.AppendString(notationName, false, false);
				this.m_DecodeState.m_CurrentInstruction.AppendString(ObxmlInstructionState.sXmlTagClosingBracket, true, false);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000040A8 File Offset: 0x000022A8
		internal void NotationDecl(DTDObject note)
		{
			this.NotationDecl(note.ObjectName, note.PublicId, note.SystemId);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000040C4 File Offset: 0x000022C4
		internal void NotationDecl(string name, string publicId, string systemId)
		{
			try
			{
				this.PrintAttributeDecl();
				this.InilializeSubset();
				this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(ObxmlInstructionState.sXmlNotation, true, false);
				this.m_DecodeState.m_CurrentInstruction.AppendString(name, false, false);
				if (!string.IsNullOrEmpty(publicId))
				{
					this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(ObxmlInstructionState.sXmlPublic, true, true);
					this.m_DecodeState.m_CurrentInstruction.AppendQuoted(publicId, true, false);
					this.m_DecodeState.m_CurrentInstruction.AppendQuoted(systemId, false, false);
				}
				else if (!string.IsNullOrEmpty(systemId))
				{
					this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(ObxmlInstructionState.sXmlSystem, true, true);
					this.m_DecodeState.m_CurrentInstruction.AppendQuoted(systemId, false, false);
				}
				this.m_DecodeState.m_CurrentInstruction.AppendString(ObxmlInstructionState.sXmlTagClosingBracket, true, false);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000041C4 File Offset: 0x000023C4
		internal string PrintAttributeDecl()
		{
			string lastElementString;
			try
			{
				if (this.m_AttributeList == null || this.m_AttributeList.Count == 0)
				{
					lastElementString = this.m_lastElementString;
				}
				else
				{
					this.InilializeSubset();
					for (int i = 0; i < this.m_AttributeList.Count; i++)
					{
						this.m_DecodeState.m_CurrentInstruction.AppendString(ObxmlInstructionState.sXmlTagAttributeStart, false, true);
						this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(this.m_lastElementString = this.m_AttributeList[i].ElementName, true, false);
						this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(this.m_AttributeList[i].AttributeName, true, false);
						if (!string.IsNullOrEmpty(this.m_AttributeList[i].AttributeString))
						{
							this.m_DecodeState.m_CurrentInstruction.AppendString(this.m_AttributeList[i].AttributeString, false, false);
						}
						else if (this.m_AttributeList[i].AttributeStringExpanded)
						{
							this.m_DecodeState.m_CurrentInstruction.AppendString(this.m_AttributeList[i].AttributeType, false, false);
							if (!string.IsNullOrEmpty(this.m_AttributeList[i].AttributeMode))
							{
								this.m_DecodeState.m_CurrentInstruction.AppendStringWithSpaces(this.m_AttributeList[i].AttributeMode, false, true);
							}
							if (!string.IsNullOrEmpty(this.m_AttributeList[i].AttributeValue))
							{
								this.m_DecodeState.m_CurrentInstruction.AppendQuoted(this.m_AttributeList[i].AttributeValue, false, true);
							}
						}
						this.m_DecodeState.m_CurrentInstruction.AppendString(ObxmlInstructionState.sXmlTagClosingBracket, true, false);
					}
					lastElementString = this.m_lastElementString;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
			finally
			{
				if (this.m_AttributeList != null)
				{
					this.m_AttributeList.Clear();
				}
			}
			return lastElementString;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000043EC File Offset: 0x000025EC
		internal override void ClearStateObject()
		{
			this.IsValid = false;
			this.m_ElementList = new List<DTDElementInfo>();
			this.m_AttributeList = new List<DTDElementAttributeInfo>();
			this.m_ObjectList = new List<DTDObject>();
			this.m_InitSubset = false;
			this.m_DecodeState = null;
			base.ObjectName = null;
		}

		// Token: 0x040000A8 RID: 168
		internal List<DTDElementInfo> m_ElementList;

		// Token: 0x040000A9 RID: 169
		internal List<DTDElementAttributeInfo> m_AttributeList;

		// Token: 0x040000AA RID: 170
		internal List<DTDObject> m_ObjectList;

		// Token: 0x040000AB RID: 171
		private bool m_InitSubset;

		// Token: 0x040000AC RID: 172
		private ObxmlDecodeState m_DecodeState;

		// Token: 0x040000AD RID: 173
		private string m_lastElementString;
	}
}
