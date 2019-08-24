namespace MobilePaySample
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Collections;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Web.Services;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;

    public partial class order_approval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string site_cd      = Request["site_cd"];	  // ����Ʈ�ڵ�
            string ordr_idxx    = Request["ordr_idxx"];   // �ֹ���ȣ
            string good_mny     = Request["good_mny"];	  // �����ݾ�
            string pay_method   = Request["pay_method"];  // ��������
            string escw_used    = Request["escw_used"];	  // ����ũ��
            string good_name    = Request["good_name"];	  // ��ǰ��
            string Ret_URL      = Request["Ret_URL"];	  // ����URL

            KCPPaymentService kcpPaymentService = new KCPPaymentService();
            ApproveRes approveRes = null;
            BaseResponseType baseResponseType = null;

            /**
             * AccessCredentialType �� ������������� �������� �ʽ��ϴ�.
             */
            AccessCredentialType accessCredentialType = new AccessCredentialType();
            accessCredentialType.accessLicense = "";
            accessCredentialType.signature = "";
            accessCredentialType.timestamp = "";

            /**
             * BaseRequestType �� ����û������ �������� ó���ԷµǴ� ����Ÿ �Դϴ�.
             */
            BaseRequestType baseRequestType = new BaseRequestType();
            baseRequestType.detailLevel = "Full";            // Full
            baseRequestType.requestApp  = "APP";             // ��:WEB, ��:APP
            baseRequestType.requestID   = "1234567890";      // �޼����ĺ� ���̵�
            baseRequestType.userAgent   = Request.UserAgent; // Ŭ���̾�Ʈ�� UserAgent
            baseRequestType.version     = "0.1";             // ����:0.1

            /**
             * ApproveReq �� ������������ �ʿ��� ����Ÿ �Դϴ�.
             */
            ApproveReq approveReq = new ApproveReq();
            approveReq.accessCredentialType = accessCredentialType;
            approveReq.baseRequestType = baseRequestType;
            approveReq.siteCode = site_cd;
            approveReq.orderID = ordr_idxx;
            approveReq.paymentAmount = good_mny;
            approveReq.paymentMethod = pay_method;
            approveReq.returnUrl = Ret_URL;
            approveReq.productName = good_name;

            bool isEscrow = false;
            if (escw_used == "Y")
            {
                isEscrow = true;
            }


            approveReq.escrow = isEscrow;
            approveReq.escrowSpecified = isEscrow;

            Console.WriteLine("escw_used:" + escw_used);
            Console.WriteLine("approveReq.escrow:"+approveReq.escrow);

            // ������û
            approveRes = kcpPaymentService.approve(approveReq);
            baseResponseType = approveRes.baseResponseType;

            string result = baseResponseType.error.code + "," + approveRes.approvalKey + "," + approveRes.payUrl + "," + baseResponseType.error.message;

            Response.ContentType = "text/html";
            Response.Write(result);
            Response.End();

        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="KCPPaymentServiceSoap11Binding", Namespace="http://webservice.act.webpay.service.kcp.kr")]
    public partial class KCPPaymentService : System.Web.Services.Protocols.SoapHttpClientProtocol {

        private System.Threading.SendOrPostCallback approveOperationCompleted;

        /// <remarks/>
        public KCPPaymentService() {
            this.Url = "https://testsmpay.kcp.co.kr/services/KCPPaymentService.KCPPaymentServiceHttpSoap11" +
                "Endpoint/";
			//�׽�Ʈ https://testsmpay.kcp.co.kr
			//����   https://smpay.kcp.co.kr
        }

        /// <remarks/>
        public event approveCompletedEventHandler approveCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:approve", RequestNamespace="http://webservice.act.webpay.service.kcp.kr", ResponseNamespace="http://webservice.act.webpay.service.kcp.kr", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public ApproveRes approve([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] ApproveReq req) {
            object[] results = this.Invoke("approve", new object[] {
                        req});
            return ((ApproveRes)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult Beginapprove(ApproveReq req, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("approve", new object[] {
                        req}, callback, asyncState);
        }

        /// <remarks/>
        public ApproveRes Endapprove(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((ApproveRes)(results[0]));
        }

        /// <remarks/>
        public void approveAsync(ApproveReq req) {
            this.approveAsync(req, null);
        }

        /// <remarks/>
        public void approveAsync(ApproveReq req, object userState) {
            if ((this.approveOperationCompleted == null)) {
                this.approveOperationCompleted = new System.Threading.SendOrPostCallback(this.OnapproveOperationCompleted);
            }
            this.InvokeAsync("approve", new object[] {
                        req}, this.approveOperationCompleted, userState);
        }

        private void OnapproveOperationCompleted(object arg) {
            if ((this.approveCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.approveCompleted(this, new approveCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://payment.domain.webpay.service.kcp.kr/xsd")]
    public partial class ApproveReq {

        private AccessCredentialType accessCredentialTypeField;

        private BaseRequestType baseRequestTypeField;

        private bool escrowField;

        private bool escrowFieldSpecified;

        private string orderIDField;

        private string paymentAmountField;

        private string paymentMethodField;

        private string productNameField;

        private string returnUrlField;

        private string siteCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public AccessCredentialType accessCredentialType {
            get {
                return this.accessCredentialTypeField;
            }
            set {
                this.accessCredentialTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public BaseRequestType baseRequestType {
            get {
                return this.baseRequestTypeField;
            }
            set {
                this.baseRequestTypeField = value;
            }
        }

        /// <remarks/>
        public bool escrow {
            get {
                return this.escrowField;
            }
            set {
                this.escrowField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool escrowSpecified {
            get {
                return this.escrowFieldSpecified;
            }
            set {
                this.escrowFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string orderID {
            get {
                return this.orderIDField;
            }
            set {
                this.orderIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string paymentAmount {
            get {
                return this.paymentAmountField;
            }
            set {
                this.paymentAmountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string paymentMethod {
            get {
                return this.paymentMethodField;
            }
            set {
                this.paymentMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string productName {
            get {
                return this.productNameField;
            }
            set {
                this.productNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string returnUrl {
            get {
                return this.returnUrlField;
            }
            set {
                this.returnUrlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string siteCode {
            get {
                return this.siteCodeField;
            }
            set {
                this.siteCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://domain.webpay.service.kcp.kr/xsd")]
    public partial class AccessCredentialType {

        private string accessLicenseField;

        private string signatureField;

        private string timestampField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string accessLicense {
            get {
                return this.accessLicenseField;
            }
            set {
                this.accessLicenseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string signature {
            get {
                return this.signatureField;
            }
            set {
                this.signatureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string timestamp {
            get {
                return this.timestampField;
            }
            set {
                this.timestampField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://domain.webpay.service.kcp.kr/xsd")]
    public partial class ErrorType {

        private string codeField;

        private string detailField;

        private string messageField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string detail {
            get {
                return this.detailField;
            }
            set {
                this.detailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://domain.webpay.service.kcp.kr/xsd")]
    public partial class BaseResponseType {

        private string detailLevelField;

        private ErrorType errorField;

        private string messageIDField;

        private string releaseField;

        private string requestIDField;

        private string responseTypeField;

        private string timestampField;

        private string versionField;

        private ErrorType[] warningListField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string detailLevel {
            get {
                return this.detailLevelField;
            }
            set {
                this.detailLevelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public ErrorType error {
            get {
                return this.errorField;
            }
            set {
                this.errorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string messageID {
            get {
                return this.messageIDField;
            }
            set {
                this.messageIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string release {
            get {
                return this.releaseField;
            }
            set {
                this.releaseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string requestID {
            get {
                return this.requestIDField;
            }
            set {
                this.requestIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string responseType {
            get {
                return this.responseTypeField;
            }
            set {
                this.responseTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string timestamp {
            get {
                return this.timestampField;
            }
            set {
                this.timestampField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("warningList", IsNullable=true)]
        public ErrorType[] warningList {
            get {
                return this.warningListField;
            }
            set {
                this.warningListField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://payment.domain.webpay.service.kcp.kr/xsd")]
    public partial class ApproveRes {

        private string approvalKeyField;

        private BaseResponseType baseResponseTypeField;

        private string payUrlField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string approvalKey {
            get {
                return this.approvalKeyField;
            }
            set {
                this.approvalKeyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public BaseResponseType baseResponseType {
            get {
                return this.baseResponseTypeField;
            }
            set {
                this.baseResponseTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string payUrl {
            get {
                return this.payUrlField;
            }
            set {
                this.payUrlField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://domain.webpay.service.kcp.kr/xsd")]
    public partial class BaseRequestType {

        private string detailLevelField;

        private string requestAppField;

        private string requestIDField;

        private string userAgentField;

        private string versionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string detailLevel {
            get {
                return this.detailLevelField;
            }
            set {
                this.detailLevelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string requestApp {
            get {
                return this.requestAppField;
            }
            set {
                this.requestAppField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string requestID {
            get {
                return this.requestIDField;
            }
            set {
                this.requestIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string userAgent {
            get {
                return this.userAgentField;
            }
            set {
                this.userAgentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    public delegate void approveCompletedEventHandler(object sender, approveCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class approveCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {

        private object[] results;

        internal approveCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState) {
            this.results = results;
        }

        /// <remarks/>
        public ApproveRes Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ApproveRes)(this.results[0]));
            }
        }
    }
}