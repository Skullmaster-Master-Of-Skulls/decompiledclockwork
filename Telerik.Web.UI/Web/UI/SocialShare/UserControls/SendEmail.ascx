<%@ Control Language="C#" AutoEventWireup="true" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div id="emailForm" class="sshSendFormWrap">
    <div class="sshForm">
        <div id="errorValidation" class="sshValidationError" style="display: none; height: 16px;">
            Please, make sure that you have entered valid email addresses!
        </div>
        <div class="sshFormRow">
            <label for="url" class="sshLabelSingle">
                Link</label>
            <input id="url" type="text" name="url" />
        </div>
        <div>
            <div class="sshFormRow">
                <label for="to">
                    Share with <span class="sshLabelBellow">recipients` e-mail</span></label>
                <textarea id="to" name="to" rows="5" cols="30"></textarea>
            </div>
            <div class="sshFormRow">
                <label for="from">
                    From <span class="sshLabelBellow">your e-mail</span>
                </label>
                <input id="from" type="text" name="from" /></div>
            <div class="sshFormRow">
                <label for="subject">
                    Subject <span class="sshLabelBellow">optional</span></label>
                <input id="subject" type="text" name="comments" />
            </div>
            <div class="sshFormRow">
                <label for="comments">
                    Comments <span class="sshLabelBellow">optional</span></label>
                <textarea id="comments" name="comments" rows="5" cols="30"></textarea>
            </div>
            <div class="sshFormRow sshCaptcha" style="margin-left:100px;">
                <telerik:RadCaptcha ID="captcha" runat="server" CaptchaImage-EnableCaptchaAudio="true" 
                    CaptchaImage-PersistCodeDuringAjax="true" EnableRefreshImage="true">
                </telerik:RadCaptcha>
                <span id="errorCaptcha" style="display: none; padding-left:0;">Please, reenter the code in the captcha!
                </span>
            </div>
            <div class="sshFormRowLast">
                <input type="button" id="sendBtn" value="Send" class="sshFormSendBtn" />
            </div>
        </div>
    </div>
</div>
<div id="success" class="sshFormSuccess" style="display: none; width: 390px;">
    <span>The link was successfully Sent!</span>
    <div class="sshBtnConfirm">
        <input type="button" id="ok" value="OK" class="sshFormButtons" /></div>
</div>
<div id="error" class="sshFormError" style="display: none; width: 390px;">
    <span>Ooops!:( An error has occurred!</span>
    <div class="sshBtnConfirm">
        <input type="button" id="tryAgain" value="Try again" class="sshFormButtons" />
        <input type="button" id="cancel" value="Cancel" class="sshFormButtons" style="margin-left: 5px;" /></div>
</div>
