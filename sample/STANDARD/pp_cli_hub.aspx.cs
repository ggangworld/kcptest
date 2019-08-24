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
using System.Xml;

using KCP.PP_CLI_COM.LIB;

namespace KCP.PP_CLI_COM.SRC
{
    public partial class pp_cli_com : System.Web.UI.Page
    {
        /* ==================================================================================== */
        /* +    변수                                                                          + */
        /* - -------------------------------------------------------------------------------- - */
        private string m_strCFG_paygw_url;      // 결제 GW URL
        private string m_strCFG_paygw_port;     // 결제 GW PORT
        private string m_strCFG_key_path;       // KCP 모듈 KEY 경로
        private string m_strCFG_log_path;       // KCP 모듈 LOG 경로 
        private string m_strCFG_site_key;       // 상점 키
        protected string m_strCFG_site_cd;      // 상점 코드
        /* - -------------------------------------------------------------------------------- - */
        private string m_strCustIP;             // 요청 IP
        private string m_strTxCD;               // 처리 종류
        private string m_strSet_user_type;      // PG1,NETPG 구분
        /* - -------------------------------------------------------------------------------- - */
        protected string amount;                // KCP 실제 거래 금액
        protected string req_tx;                // 요청 종류
        protected string bSucc;                 // 업체 처리 성공 유무
        protected string use_pay_method;        // 사용한 결제 수단
        protected string ordr_idxx;             // 주문 번호
        protected string good_name;             // 상품명
        protected string buyr_name;             // 구매자명
        protected string buyr_mail;             // 구매자 메일
        protected string buyr_tel1;             // 구매자 연락처 1
        protected string buyr_tel2;             // 구매자 연락처 2
        /* - -------------------------------------------------------------------------------- - */
        protected string trad_numb;
        protected string enct_info;
        protected string enct_data;
        protected string ordr_mony;
        /* - -------------------------------------------------------------------------------- - */
        protected string res_cd;
        protected string res_msg;
        /* - -------------------------------------------------------------------------------- - */
        protected string mod_type;              // 변경TYPE(승인취소시 필요)
        protected string tno;                   // KCP 거래 고유 번호
        protected string mod_desc;              // 변경 사유
        protected string mod_mny;               // 취소요청금액
        protected string rem_mny;               // 취소가능잔액
        protected string panc_mod_mny;          // 부분취소 금액
        protected string panc_rem_mny;          // 부분취소 가능 금액]
        protected string mod_tax_mny;           // 공급가 부분 취소 요청 금액
        protected string mod_vat_mny;           // 부과세 부분 취소 요청 금액
        protected string mod_free_mny;          // 비과세 부분 취소 요청 금액
        protected string tax_flag;              // 복합과세 구분
        /* - -------------------------------------------------------------------------------- - */
        protected string m_strResCD;            // 응답코드
        protected string m_strResMsg;           // 응답메시지
        /* - -------------------------------------------------------------------------------- - */
        protected string commid;                // 통신사 코드
        protected string mobile_no;             // 휴대폰 번호
        /* - -------------------------------------------------------------------------------- - */
        protected string tk_van_code;           // 발급사 코드
        protected string tk_app_no;             // 승인 번호
        protected string tk_app_time;           // 상품권 승인시간
        protected string shop_user_id;          // 가맹점 고객 아이디
        /* - -------------------------------------------------------------------------------- - */
        protected string card_cd;               // 카드사 코드
        protected string card_name;             // 카드사 명
        protected string app_time;              // 승인시간
        protected string app_no;                // 승인번호
        protected string noinf;                 // 무이자 여부
        protected string quota;                 // 할부 개월 수
        protected string partcanc_yn;           // 부분취소가능여부
        protected string card_bin_type_01;      // 카드구분1
        protected string card_bin_type_02;      // 카드구분2
        protected string card_mny;              // 카드결제금액 
        /* - -------------------------------------------------------------------------------- - */
        protected string add_pnt;               // 발생 포인트
        protected string use_pnt;               // 사용가능 포인트
        protected string rsv_pnt;               // 적립 포인트
        protected string pnt_app_time;          // 승인시간
        protected string pnt_app_no;            // 승인번호
        protected string pnt_amount;            // 적립금액 or 사용금액
        protected string pnt_issue;             // 포인트 결제사
        /* - -------------------------------------------------------------------------------- - */
        protected string bank_name;             // 은행명
        protected string bank_code;             // 은행코드
        protected string bk_mny;                // 계좌이체결제금액 
        /* - -------------------------------------------------------------------------------- - */
        protected string bankname;              // 입금할 은행 이름
        protected string depositor;             // 입금할 계좌 예금주
        protected string account;               // 입금할 계좌 번호
        protected string va_date;               // 가상계좌 입금마감시간
        /* - -------------------------------------------------------------------------------- - */
        protected string hp_app_time;           // 휴대폰 승인시간
        protected string hp_commid;             // 통신사 코드
        protected string hp_mobile_noo;         // 휴대폰 번호
        /* - -------------------------------------------------------------------------------- - */
        protected string ars_app_time;          // ARS 승인시간
        /* - -------------------------------------------------------------------------------- - */
        protected string cash_yn;               // 현금 영수증 등록 여부
        protected string cash_authno;           // 현금 영수증 승인 번호
        protected string cash_tr_code;          // 현금 영수증 발행 구분
        protected string cash_id_info;          // 현금 영수증 등록 번호     
        protected string cash_no;               // 현금 영수증 거래 번호         
        protected string good_mny; 		
        /* - -------------------------------------------------------------------------------- - */
        protected void Page_Load(object sender, EventArgs e)
        {
            /* -------------------------------------------------------------------------------- */
            /* +    DATA 설정                                                                 + */
            /* - ---------------------------------------------------------------------------- - */
            m_f__get_param();
            /* - ---------------------------------------------------------------------------- - */
            
            /* -------------------------------------------------------------------------------- */
            /* +    가맹점 정보 READ                                                          + */
            /* - ---------------------------------------------------------------------------- - */
            m_f__load_env();    // 가맹점 고유 환경설정
            /* -------------------------------------------------------------------------------- */

            m_f__do_tx();
        }

        /* ==================================================================================== */
        /* +    METHOD : GET POST DATA                                                        + */
        /* - -------------------------------------------------------------------------------- - */
        private void m_f__get_param()
        {
            req_tx         = m_f__get_post_data("req_tx");
            use_pay_method = m_f__get_post_data("use_pay_method");
            ordr_idxx     = m_f__get_post_data("ordr_idxx");
            good_name     = m_f__get_post_data("good_name");
            
            card_cd       = m_f__get_post_data("card_cd");

            buyr_name     = m_f__get_post_data("buyr_name");
            buyr_tel1     = m_f__get_post_data("buyr_tel1");
            buyr_tel2     = m_f__get_post_data("buyr_tel2");
            buyr_mail     = m_f__get_post_data("buyr_mail");
            /* - ---------------------------------------------------------------------------- - */
            trad_numb     = m_f__get_post_data("trace_no");
            enct_info     = m_f__get_post_data("enc_info");
            enct_data     = m_f__get_post_data("enc_data");
            /* - ---------------------------------------------------------------------------- - */
            mod_type      = m_f__get_post_data("mod_type");
            mod_mny       = m_f__get_post_data("mod_mny");
            rem_mny       = m_f__get_post_data("rem_mny");
            mod_tax_mny   = m_f__get_post_data("mod_tax_mny");  // 공급가 부분 취소 요청 금액
            mod_vat_mny   = m_f__get_post_data("mod_vat_mny");  // 부과세 부분 취소 요청 금액
            mod_free_mny  = m_f__get_post_data("mod_free_mny"); // 비과세 부분 취소 요청 금액
            tno           = m_f__get_post_data("tno");
            mod_desc      = m_f__get_post_data("mod_desc");
            pnt_issue     = m_f__get_post_data("pnt_issue");       // 포인트(OK캐쉬백,복지포인트)
            /* - ---------------------------------------------------------------------------- - */
            m_strCustIP   = Request.ServerVariables.Get("REMOTE_ADDR");
            /* -------------------------------------------------------------------------------- */
            cash_yn       = m_f__get_post_data("cash_yn");         // 현금 영수증 등록 여부
            cash_authno   = m_f__get_post_data("cash_authno");     // 현금 영수증 승인 번호
            cash_tr_code  = m_f__get_post_data("cash_tr_code");    // 현금 영수증 발행 구분
            cash_id_info  = m_f__get_post_data("cash_id_info");    // 현금 영수증 등록 번호       
			good_mny = m_f__get_post_data("good_mny");
        }
        /* ==================================================================================== */

        /* ==================================================================================== */
        /* +    METHOD : GET POST DATA                                                        + */
        /* - -------------------------------------------------------------------------------- - */
        private string m_f__get_post_data(string parm_strName)
        {
            string strRT;

            strRT = Request.Form[parm_strName];

            if (strRT == null) strRT = "";

            return strRT;
        }
        /* ==================================================================================== */

        /* ==================================================================================== */
        /* +    METHOD : 환경 설정                                                            + */
        /* - -------------------------------------------------------------------------------- - */
        private void m_f__load_env()
        {
            /* -------------------------------------------------------------------------------- */
            /* +    환경설정 DATA 설정                                                        + */
            /* - ---------------------------------------------------------------------------- - */
            /* GET WEB.CONFIG DATA */
            m_strCFG_paygw_url  = ConfigurationManager.AppSettings["g_conf_gw_url"   ];
            m_strCFG_paygw_port = ConfigurationManager.AppSettings["g_conf_gw_port"  ];
            m_strCFG_key_path   = ConfigurationManager.AppSettings["g_kcp_key_path"  ];
            m_strCFG_log_path   = ConfigurationManager.AppSettings["g_kcp_log_path"  ];
            m_strCFG_site_cd    = ConfigurationManager.AppSettings["g_conf_site_cd"  ];
            m_strCFG_site_key   = ConfigurationManager.AppSettings["g_conf_site_key" ];
            m_strSet_user_type  = ConfigurationManager.AppSettings["g_conf_user_type"];
            /* - ---------------------------------------------------------------------------- - */
        }
        /* ==================================================================================== */

        /* ==================================================================================== */
        /* +    METHOD : 요청 거래 처리                                                       + */
        /* - -------------------------------------------------------------------------------- - */
        private void m_f__do_tx()
        {
            C_PP_CLI_COM c_PP_CLI = new C_PP_CLI_COM();
            int nDataSetInx_req;
            bool bRT = false;
            bool bNetCan = false;

            /* -------------------------------------------------------------------------------- */
            /* +    초기화                                                                    + */
            /* - ---------------------------------------------------------------------------- - */
            m_strTxCD = "";
            nDataSetInx_req = 0;
            /* - ---------------------------------------------------------------------------- - */
            c_PP_CLI.m_f__set_env(m_strCFG_paygw_url, m_strCFG_paygw_port,
                                   m_strCFG_log_path, m_strCFG_key_path);
                                   
            m_strCustIP = Request.ServerVariables.Get("REMOTE_ADDR");                                   
            /* - ---------------------------------------------------------------------------- - */
            c_PP_CLI.m_f__init();
            /* -------------------------------------------------------------------------------- */

            /* -------------------------------------------------------------------------------- */
            /* +    요청 처리                                                                 + */
            /* - ---------------------------------------------------------------------------- - */
            if (req_tx.Equals("pay"))
            {
                nDataSetInx_req = m_f__set_dataset_pay(ref c_PP_CLI);
            }
            /* - ---------------------------------------------------------------------------- - */
            if (!m_strTxCD.Equals(""))
            {
                c_PP_CLI.m_f__do_tx(req_tx, m_strTxCD, nDataSetInx_req, "",
                                     m_strCFG_site_cd, m_strCFG_site_key, ordr_idxx); 

                m_strResCD  = c_PP_CLI.m_strResCD;
                m_strResMsg = c_PP_CLI.m_strResMsg;
            }
            else
            {
                m_strResCD  = "9562";
                m_strResMsg = "지불모듈 연동 오류 (TX_CD) 가 정의되지 않았습니다.";
            }
            /* -------------------------------------------------------------------------------- */

            /* -------------------------------------------------------------------------------- */
            /* +    결과 처리                                                                 + */
            /* - ---------------------------------------------------------------------------- - */
            if (m_strResCD.Equals("0000"))
            {
                /* - ------------------------------------------------------------------------ - */
                if (req_tx.Equals("pay"))
                {
                    /* - -------------------------------------------------------------------- - */
                    bRT = m_f__to_do_shop_pay();          /* 정상 적립/조회/사용 거래 결과 처리 */
                    /* - -------------------------------------------------------------------- - */
                    if (bRT == false)
                    {
                        /* - ---------------------------------------------------------------- - */
                        /* +    TODO (주위) : 망상 취소 처리                                  + */
                        /* -- -------------------------------------------------------------- -- */
                        /* +    적립/사용 결과 처리 중 오류가 발생한 경우 자동 취소를         + */
                        /* +    원하실 경우 아래의 bNetCan 값을 true로 설정하여 주시기        + */
                        /* +    바랍니다. 취소 처리는 거래는 원복을 할 수 없으므로 주위       + */
                        /* +    하여 주시기 바랍니다.                                         + */
                        /* -- -------------------------------------------------------------- -- */
                        bNetCan = false;
                        /* - ---------------------------------------------------------------- - */
                    }
                    /* - -------------------------------------------------------------------- - */
                    if (bNetCan == true)
                    {
                        m_f__do_net_can(ref c_PP_CLI);
                    }
                    else
                    {
                        m_f__disp_rt_pay_succ(ref c_PP_CLI);
                    }
                    /* - -------------------------------------------------------------------- - */
                }
                /* - ------------------------------------------------------------------------ - */
            }
            else
            {
                /* - ------------------------------------------------------------------------ - */
                m_f__to_do_shop_fail();
                /* - ------------------------------------------------------------------------ - */
                m_f__disp_rt_fail();
            }
            /* -------------------------------------------------------------------------------- */
        }
        /* ==================================================================================== */

        /* ==================================================================================== */
        /* +    METHOD : 망상 취소 처리                                                       + */
        /* - -------------------------------------------------------------------------------- - */
        private bool m_f__do_net_can(ref C_PP_CLI_COM parm_c_PP_CLI)
        {
            int nDataSetInx_req;
            bool bDoNetCan = false;

            /* -------------------------------------------------------------------------------- */
            /* +    망상 취소 DATA 설정                                                       + */
            /* - ---------------------------------------------------------------------------- - */
            mod_type = "STSC";
            tno      = parm_c_PP_CLI.m_f__get_res("tno");
            /* - ---------------------------------------------------------------------------- - */
            parm_c_PP_CLI.m_f__init();
            /* - ---------------------------------------------------------------------------- - */
            if (req_tx.Equals("pay"))
            {

                bDoNetCan = true;
                mod_desc  = "처리 오류로 인한 거래 자동 취소";
            }
            /* -------------------------------------------------------------------------------- */

            /* -------------------------------------------------------------------------------- */
            /* +    자동 취소 처리                                                            + */
            /* - ---------------------------------------------------------------------------- - */
            if (bDoNetCan == true)
            {
                nDataSetInx_req = m_f__set_dataset_mod(ref parm_c_PP_CLI);
                parm_c_PP_CLI.m_f__do_tx(req_tx, m_strTxCD, nDataSetInx_req, "", m_strCFG_site_cd, m_strCFG_site_key, ordr_idxx);
                m_strResCD  = parm_c_PP_CLI.m_strResCD;
                m_strResMsg = parm_c_PP_CLI.m_strResMsg;
                
                m_f__disp_rt_can(ref parm_c_PP_CLI);
            }
            /* -------------------------------------------------------------------------------- */

            return bDoNetCan;
        }
        /* ==================================================================================== */

        /* ==================================================================================== */
        /* +    METHOD : 요청 DATA 생성                                                       + */
        /* - -------------------------------------------------------------------------------- - */
        private int m_f__set_dataset_pay(ref C_PP_CLI_COM parm_c_PP_CLI)
        {
            /* -------------------------------------------------------------------------------- */
            /* +    지불 요청 DATA 구성                                                       + */
            /* - ---------------------------------------------------------------------------- - */
            m_strTxCD = m_f__get_post_data("tran_cd");

            /* -------------------------------------------------------------------------------- */
            /* +    금액 위변조 방지 관련 추가 (2010.12)                                           + */
            /* - ---------------------------------------------------------------------------- - */
            int nDataSetInx_req;
            int nDataSetInx_ordr_no;


            nDataSetInx_req = parm_c_PP_CLI.m_f__get_dataset("plan_data");
            nDataSetInx_ordr_no = parm_c_PP_CLI.m_f__get_dataset("ordr_data");
            /* 1 원은 실제로 업체에서 결제하셔야 될 원 금액을 넣어주셔야 합니다. 결제금액 유효성 검증 */
            parm_c_PP_CLI.m_f__set_data(nDataSetInx_ordr_no, "ordr_mony", good_mny );

            parm_c_PP_CLI.m_f__add_data(nDataSetInx_req, nDataSetInx_ordr_no, "\x1c");


            return parm_c_PP_CLI.m_f__set_axdataset(trad_numb, enct_info, enct_data);
        }

        /* ==================================================================================== */
        /* +    METHOD : 요청 DATA 생성                                                       + */
        /* - -------------------------------------------------------------------------------- - */
        private int m_f__set_dataset_mod(ref C_PP_CLI_COM parm_c_PP_CLI)
        {
            int nDataSetInx_req;
            int nDataSetInx_mod;


            /* -------------------------------------------------------------------------------- */
            /* +    변경 요청 DATA 구성                                                       + */
            /* - ---------------------------------------------------------------------------- - */
            m_strTxCD = "00200000";
            /* - ---------------------------------------------------------------------------- - */
            nDataSetInx_req = parm_c_PP_CLI.m_f__get_dataset("plan_data");
            nDataSetInx_mod = parm_c_PP_CLI.m_f__get_dataset("mod_data");
            /* - ---------------------------------------------------------------------------- - */
            parm_c_PP_CLI.m_f__set_data(nDataSetInx_mod, "mod_type", mod_type);
            parm_c_PP_CLI.m_f__set_data(nDataSetInx_mod, "tno", tno);
            parm_c_PP_CLI.m_f__set_data(nDataSetInx_mod, "mod_ip", m_strCustIP);
            parm_c_PP_CLI.m_f__set_data(nDataSetInx_mod, "mod_desc", mod_desc);
            /* - ---------------------------------------------------------------------------- - */
            parm_c_PP_CLI.m_f__add_data(nDataSetInx_req, nDataSetInx_mod, "\x1c");
            /* -------------------------------------------------------------------------------- */

            return nDataSetInx_req;
        }
        /* ==================================================================================== */

        /* ==================================================================================== */
        /* +    METHOD : 정상 결제 결과 처리                                                  + */
        /* - -------------------------------------------------------------------------------- - */
        private bool m_f__to_do_shop_pay()
        {
            bool bRT = true;

            /* -------------------------------------------------------------------------------- */
            /* +    결과 처리                                                                 + */
            /* - ---------------------------------------------------------------------------- - */

            /* -------------------------------------------------------------------------------- */
            /* +    TODO (필수) : 처리 결과가 정상인 경우에는 반드시 bRT 값을 true로,         + */
            /* +                  오류가 발생한 경우에는 false 로 설정하여 주시기 바랍니다.   + */
            /* - ---------------------------------------------------------------------------- - */
            bRT = true;                  /* 정상 처리인 경우 : true, 오류가 발생한 경우 : false */
            /* -------------------------------------------------------------------------------- */

            return bRT;
        }
        /* ==================================================================================== */


        /* ==================================================================================== */
        /* +    METHOD : 오류 결과 처리                                                       + */
        /* - -------------------------------------------------------------------------------- - */
        private bool m_f__to_do_shop_fail()
        {
            bool bRT = true;

            /* -------------------------------------------------------------------------------- */
            /* +    거래 종류 별 오류 결과 처리                                               + */
            /* - ---------------------------------------------------------------------------- - */
            if (req_tx.Equals("pay"))
            {
                /* - ------------------------------------------------------------------------ - */
                /* +    TODO : 승인 거래 오류 시 처리                                         + */
                /* - ------------------------------------------------------------------------ - */
            }
            else if (req_tx.Equals("mod"))
            {
                /* - ------------------------------------------------------------------------ - */
                /* +    TODO : 취소 거래 오류 시 처리                                         + */
                /* - ------------------------------------------------------------------------ - */
            }
            /* -------------------------------------------------------------------------------- */

            return bRT;
        }
        /* ==================================================================================== */

        /* ==================================================================================== */
        /* +    METHOD : 결과 출력 (적립/조회/사용 정상)                                      + */
        /* - -------------------------------------------------------------------------------- - */
        private void m_f__disp_rt_pay_succ(ref C_PP_CLI_COM parm_c_PP_CLI)
        {
            /* -------------------------------------------------------------------------------- */
            /* +    정상 결과 출력                                                            + */
            /* - ---------------------------------------------------------------------------- - */
            res_cd  = m_strResCD;
            res_msg = m_strResMsg;
            /* - ---------------------------------------------------------------------------- - */
            ordr_idxx    = ordr_idxx;
            tno          = parm_c_PP_CLI.m_f__get_res("tno");
            amount       = parm_c_PP_CLI.m_f__get_res("amount");      // KCP 실제 거래 금액
            good_name    = good_name;
            buyr_name    = buyr_name;
            buyr_tel1    = buyr_tel1;
            buyr_tel2    = buyr_tel2;
            buyr_mail    = buyr_mail;
            //coupon_mny = parm_c_PP_CLI.m_f__get_res("coupon_mny");  // 쿠폰금액

            /* - ---------------------------------------------------------------------------- - */
            if (use_pay_method == "100000000000")
            {
                //신용카드
                card_cd   = parm_c_PP_CLI.m_f__get_res("card_cd");       // 카드사 코드
                card_name = parm_c_PP_CLI.m_f__get_res("card_name");     // 카드사 명
                app_time  = parm_c_PP_CLI.m_f__get_res("app_time");      // 승인시간
                app_no    = parm_c_PP_CLI.m_f__get_res("app_no");        // 승인번호
                noinf     = parm_c_PP_CLI.m_f__get_res("noinf");         // 무이자 여부
                quota     = parm_c_PP_CLI.m_f__get_res("quota");         // 할부 개월 수
                partcanc_yn = parm_c_PP_CLI.m_f__get_res("partcanc_yn");            //  부분취소가능여부
                card_bin_type_01 = parm_c_PP_CLI.m_f__get_res("card_bin_type_01");  //  카드구분1
                card_bin_type_02 = parm_c_PP_CLI.m_f__get_res("card_bin_type_02");  //  카드구분2
                card_mny         = parm_c_PP_CLI.m_f__get_res("card_mny"   );       //  카드결제금액 
                pnt_issue        = parm_c_PP_CLI.m_f__get_res("pnt_issue"  );       //  포인트 서비스사

                if (pnt_issue == "SCSK" || pnt_issue == "SCWB")
                {
                    // 복합합결제
                    add_pnt      = parm_c_PP_CLI.m_f__get_res("add_pnt");           // 발생 포인트
                    use_pnt      = parm_c_PP_CLI.m_f__get_res("use_pnt");           // 사용가능 포인트
                    rsv_pnt      = parm_c_PP_CLI.m_f__get_res("rsv_pnt");           // 적립 포인트
                    pnt_app_time = parm_c_PP_CLI.m_f__get_res("pnt_app_time");      // 승인시간
                    pnt_app_no   = parm_c_PP_CLI.m_f__get_res("pnt_app_no");        // 승인번호
                    pnt_amount   = parm_c_PP_CLI.m_f__get_res("pnt_amount");        // 적립금액 or 사용금액
                    pnt_issue    = parm_c_PP_CLI.m_f__get_res("pnt_issue");   
                }
            }
            else if (use_pay_method == "010000000000")
            {
                //계좌이체
                bank_name = parm_c_PP_CLI.m_f__get_res("bank_name");           // 은행명
                bank_code = parm_c_PP_CLI.m_f__get_res("bank_code");           // 은행코드
                bk_mny    = parm_c_PP_CLI.m_f__get_res("bk_mny"   );           // 계좌이체결제금액
                app_time  = parm_c_PP_CLI.m_f__get_res("app_time");            // 승인시간
            }
            else if (use_pay_method == "001000000000")
            {
                //가상계좌
                bankname  = parm_c_PP_CLI.m_f__get_res("bankname");            // 입금할 은행 이름
                depositor = parm_c_PP_CLI.m_f__get_res("depositor");           // 입금할 계좌 예금주
                account   = parm_c_PP_CLI.m_f__get_res("account");             // 입금할 계좌 번호
                va_date   = parm_c_PP_CLI.m_f__get_res("va_date");             // 가상계좌 입금마감시간
            }
            else if (use_pay_method == "000100000000")
            {
                //포인트
                add_pnt      = parm_c_PP_CLI.m_f__get_res("add_pnt");          // 발생 포인트
                use_pnt      = parm_c_PP_CLI.m_f__get_res("use_pnt");          // 사용가능 포인트
                rsv_pnt      = parm_c_PP_CLI.m_f__get_res("rsv_pnt");          // 적립 포인트
                pnt_app_time = parm_c_PP_CLI.m_f__get_res("pnt_app_time");     // 승인시간
                pnt_app_no   = parm_c_PP_CLI.m_f__get_res("pnt_app_no");       // 승인번호
                pnt_amount   = parm_c_PP_CLI.m_f__get_res("pnt_amount");       // 적립금액 or 사용금액
                pnt_issue    = parm_c_PP_CLI.m_f__get_res("pnt_issue");        // 포인트 결제사
            }
            else if (use_pay_method == "000010000000")
            {
                //휴대폰
                hp_app_time  = parm_c_PP_CLI.m_f__get_res("hp_app_time");       // 휴대폰 승인시간
                commid    = parm_c_PP_CLI.m_f__get_res("commid");               // 통신사 코드
                mobile_no = parm_c_PP_CLI.m_f__get_res("mobile_no");            // 휴대폰 번호
                app_time  = parm_c_PP_CLI.m_f__get_res("app_time");             // 승인시간
            }
            else if (use_pay_method == "000000001000")
            {                
                //상품권
                tk_van_code = parm_c_PP_CLI.m_f__get_res("tk_van_code");        // 발급사 코드
                tk_app_no   = parm_c_PP_CLI.m_f__get_res("tk_app_no");          // 승인 번호
                app_time    = parm_c_PP_CLI.m_f__get_res("tk_app_time");        // 승인시간
            }
            else if (use_pay_method == "000000000010")
            {
                //ARS
                ars_app_time = parm_c_PP_CLI.m_f__get_res("ars_app_time");      // ARS 승인시간
            }

            //현금영수증
            cash_yn = cash_yn;                                                  // 현금 영수증 등록 여부
            if (cash_yn == "Y")
            {

                cash_authno  = parm_c_PP_CLI.m_f__get_res("cash_authno");       // 현금 영수증 승인 번호
                cash_tr_code = cash_tr_code;                                    // 현금 영수증 발행 구분
                cash_id_info = cash_id_info;                                    // 현금 영수증 등록 번호
                cash_no      = parm_c_PP_CLI.m_f__get_res("cash_no");           // 현금 영수증 거래 번호

                }
        }
        /* ==================================================================================== */

        /* ==================================================================================== */
        /* +    METHOD : 결과 출력 (오류)                                                     + */
        /* - -------------------------------------------------------------------------------- - */
        private void m_f__disp_rt_fail()
        {
            /* -------------------------------------------------------------------------------- */
            /* +    오류 결과 출력                                                            + */
            /* - ---------------------------------------------------------------------------- - */
            res_cd  = m_strResCD;
            res_msg = m_strResMsg;
            /* -------------------------------------------------------------------------------- */
        }
        /* ==================================================================================== */

        /* ==================================================================================== */
        /* +    METHOD : 결과 출력 (취소)                                                     + */
        /* - -------------------------------------------------------------------------------- - */
        private void m_f__disp_rt_can(ref C_PP_CLI_COM parm_c_PP_CLI)
        {
            /* -------------------------------------------------------------------------------- */
            /* +    오류 결과 출력                                                            + */
            /* - ---------------------------------------------------------------------------- - */
            res_cd  = m_strResCD;
            res_msg = m_strResMsg;
            /* -------------------------------------------------------------------------------- */
        }
        /* ==================================================================================== */
    }
}
