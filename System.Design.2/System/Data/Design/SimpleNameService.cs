using System;
using System.Collections;
using System.Design;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Data.Design
{
	// Token: 0x02000261 RID: 609
	internal class SimpleNameService : INameService
	{
		// Token: 0x06001763 RID: 5987 RVA: 0x00081584 File Offset: 0x0007F784
		internal SimpleNameService()
		{
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x0008159E File Offset: 0x0007F79E
		internal static SimpleNameService DefaultInstance
		{
			get
			{
				if (SimpleNameService.defaultInstance == null)
				{
					SimpleNameService.defaultInstance = new SimpleNameService();
				}
				return SimpleNameService.defaultInstance;
			}
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x000815B6 File Offset: 0x0007F7B6
		public string CreateUniqueName(INamedObjectCollection container, string proposed)
		{
			if (!this.NameExist(container, proposed))
			{
				this.ValidateName(proposed);
				return proposed;
			}
			return this.CreateUniqueName(container, proposed, 1);
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x000815D4 File Offset: 0x0007F7D4
		public string CreateUniqueName(INamedObjectCollection container, Type type)
		{
			return this.CreateUniqueName(container, type.Name, 1);
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x000815E4 File Offset: 0x0007F7E4
		public string CreateUniqueName(INamedObjectCollection container, string proposedNameRoot, int startSuffix)
		{
			return this.CreateUniqueNameOnCollection(container, proposedNameRoot, startSuffix);
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x000815F0 File Offset: 0x0007F7F0
		public string CreateUniqueNameOnCollection(ICollection container, string proposedNameRoot, int startSuffix)
		{
			int num = startSuffix;
			if (num < 0)
			{
				num = 0;
			}
			this.ValidateName(proposedNameRoot);
			string text = proposedNameRoot + num.ToString(CultureInfo.CurrentCulture);
			while (this.NameExist(container, text))
			{
				num++;
				if (num >= this.maxNumberOfTrials)
				{
					throw new InternalException("Failed to create unique name after many attempts", 1, true);
				}
				text = proposedNameRoot + num.ToString(CultureInfo.CurrentCulture);
			}
			this.ValidateName(text);
			return text;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00081660 File Offset: 0x0007F860
		private bool NameExist(ICollection container, string nameTobeChecked)
		{
			return this.NameExist(container, null, nameTobeChecked);
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0008166C File Offset: 0x0007F86C
		private bool NameExist(ICollection container, INamedObject objTobeChecked, string nameTobeChecked)
		{
			if (StringUtil.Empty(nameTobeChecked) && objTobeChecked != null)
			{
				nameTobeChecked = objTobeChecked.Name;
			}
			foreach (object obj in container)
			{
				INamedObject namedObject = (INamedObject)obj;
				if (namedObject != objTobeChecked && StringUtil.EqualValue(namedObject.Name, nameTobeChecked, !this.caseSensitive))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x000816F0 File Offset: 0x0007F8F0
		public virtual void ValidateName(string name)
		{
			if (StringUtil.EmptyOrSpace(name))
			{
				throw new NameValidationException(SR.GetString("CM_NameNotEmptyExcption"));
			}
			if (name.Length > 1024)
			{
				throw new NameValidationException(SR.GetString("CM_NameTooLongExcption"));
			}
			Match match = Regex.Match(name, SimpleNameService.regexIdentifier);
			if (!match.Success)
			{
				throw new NameValidationException(SR.GetString("CM_NameInvalid", new object[]
				{
					name
				}));
			}
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00081760 File Offset: 0x0007F960
		public void ValidateUniqueName(INamedObjectCollection container, string proposedName)
		{
			this.ValidateUniqueName(container, null, proposedName);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0008176B File Offset: 0x0007F96B
		public void ValidateUniqueName(INamedObjectCollection container, INamedObject namedObject, string proposedName)
		{
			this.ValidateName(proposedName);
			if (this.NameExist(container, namedObject, proposedName))
			{
				throw new NameValidationException(SR.GetString("CM_NameExist", new object[]
				{
					proposedName
				}));
			}
		}

		// Token: 0x04000BF3 RID: 3059
		internal const int DEFAULT_MAX_TRIALS = 100000;

		// Token: 0x04000BF4 RID: 3060
		private const int MAX_LENGTH = 1024;

		// Token: 0x04000BF5 RID: 3061
		private int maxNumberOfTrials = 100000;

		// Token: 0x04000BF6 RID: 3062
		private static readonly string regexAlphaCharacter = "[\\p{L}\\p{Nl}]";

		// Token: 0x04000BF7 RID: 3063
		private static readonly string regexUnderscoreCharacter = "\\p{Pc}";

		// Token: 0x04000BF8 RID: 3064
		private static readonly string regexIdentifierCharacter = "[\\p{L}\\p{Nl}\\p{Nd}\\p{Mn}\\p{Mc}\\p{Cf}]";

		// Token: 0x04000BF9 RID: 3065
		private static readonly string regexIdentifierStart = string.Concat(new string[]
		{
			"(",
			SimpleNameService.regexAlphaCharacter,
			"|(",
			SimpleNameService.regexUnderscoreCharacter,
			SimpleNameService.regexIdentifierCharacter,
			"))"
		});

		// Token: 0x04000BFA RID: 3066
		private static readonly string regexIdentifier = SimpleNameService.regexIdentifierStart + SimpleNameService.regexIdentifierCharacter + "*";

		// Token: 0x04000BFB RID: 3067
		private static SimpleNameService defaultInstance;

		// Token: 0x04000BFC RID: 3068
		private bool caseSensitive = true;
	}
}
