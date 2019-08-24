using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace KCP.PP_CLI_COM.LIB
{
    /// <summary>
    /// C_PP_CLI의 요약 설명입니다.
    /// </summary>
    /// 
    public class C_PP_CLI_COM
    {
        /* =============================================================== */
        /* +    변수                                                     + */
        /* - ----------------------------------------------------------- - */
        PP_CLI_COMLib.KCP m_c_Payplus;
        /* - ----------------------------------------------------------- - */
        private string m_strKCPGWUrl;
        private string m_strKCPGWPort;
        private string m_strLogPath;
        private string m_strKeyPath;
        /* - ----------------------------------------------------------- - */
        const int cnDataSetSize = 32;
        /* - ----------------------------------------------------------- - */
        private string[,] m_straDataSet_req;
        private Int32 m_nDataSetCnt_req;
        private string m_strDataSet_res;
        private string[,] m_straResData;
        private Int32 m_nResDataCnt;
        /* - ----------------------------------------------------------- - */
        private string m_strDataSet_enc;
        private string m_strDataSet_trace_no;
        private string m_strDataSet_enc_info;
        /* - ----------------------------------------------------------- - */
        public string m_strResCD;
        public string m_strResMsg;
        /* =============================================================== */

        public C_PP_CLI_COM()
        {
            //
            // TODO: 생성자 논리를 여기에 추가합니다.
            //
            m_c_Payplus = null;

            m_f__init_data();
        }

        /* =============================================================== */
        /* +    초기화                                                   + */
        /* - ----------------------------------------------------------- - */
        public bool m_f__init()
        {
            bool bRT = true;

            if (m_c_Payplus == null)
            {
                m_c_Payplus = new PP_CLI_COMLib.KCP();
                if (m_c_Payplus == null) 
                    bRT = false;
            }

            if (bRT == true)
            {
                m_c_Payplus.lf_PP_CLI_LIB__init(m_strKeyPath, 
                    m_strLogPath, 3, m_strKCPGWUrl, m_strKCPGWPort);
                m_f__init_data();
            }

            return bRT;
        }
        /* =============================================================== */

        /* =============================================================== */
        /* +    CLEANUP                                                  + */
        /* - ----------------------------------------------------------- - */
        public void m_f__cleanup()
        {
            m_f__init_data();
        }
        /* =============================================================== */

        /* =============================================================== */
        /* +    초기화                                                   + */
        /* - ----------------------------------------------------------- - */
        public void m_f__init_data()
        {
            int nInx;

            m_straDataSet_req = new string[cnDataSetSize, 2];

            for (nInx = 0; nInx < cnDataSetSize; nInx++)
            {
                m_straDataSet_req[nInx, 0] = "";
                m_straDataSet_req[nInx, 1] = "";
            }

            m_nDataSetCnt_req = 0;

            if (m_straResData != null)
            {
                for (nInx = 0; nInx < m_nResDataCnt; nInx++)
                {
                    m_straResData[nInx, 0] = "";
                    m_straResData[nInx, 1] = "";
                }
            }

            m_strDataSet_res = "";
            m_strResCD = "XXXX";
            m_strResMsg = "";
            m_nResDataCnt = 0;

            m_strDataSet_enc = "";
            m_strDataSet_trace_no = "";
            m_strDataSet_enc_info = "";
        }
        /* =============================================================== */

        /* =============================================================== */
        /* +    환경 설정                                                + */
        /* - ----------------------------------------------------------- - */
        public void m_f__set_env(string parm_strKCPGWUrl,
                                      string parm_strKCPGWPort,
                                      string parm_strLogPath,
                                      string parm_strKeyPath)
        {
            m_strKCPGWUrl = parm_strKCPGWUrl;
            m_strKCPGWPort = parm_strKCPGWPort;
            m_strLogPath = parm_strLogPath;
            m_strKeyPath = parm_strKeyPath;
        }
        /* =============================================================== */

        /* =============================================================== */
        /* +    DATA SET 생성                                            + */
        /* - ----------------------------------------------------------- - */
        public int m_f__get_dataset(string parm_strDataSetName)
        {
            int nInx;

            for (nInx = 0; nInx < m_nDataSetCnt_req; nInx++)
            {
                if (m_straDataSet_req[nInx, 0].Equals(parm_strDataSetName))
                {
                    break;
                }
            }

            if (nInx == m_nDataSetCnt_req)
            {
                m_straDataSet_req[m_nDataSetCnt_req, 0] = parm_strDataSetName;
                m_straDataSet_req[m_nDataSetCnt_req++, 1] = "";
            }

            return nInx;
        }
        /* =============================================================== */

        /* =============================================================== */
        /* +    AX_DATA SET 생성                                        + */
        /* - ----------------------------------------------------------- - */
        public int m_f__set_axdataset(string parm_strTraceNo, string parm_strEncInfo, string parm_strEncData)
        {
            int nInx = 0;

            m_strDataSet_trace_no = parm_strTraceNo;
            m_strDataSet_enc_info = parm_strEncInfo;
            m_strDataSet_enc = parm_strEncData;

            return nInx;
        }
        /* =============================================================== */

        /* =============================================================== */
        /* +    DATA 설정                                                + */
        /* - ----------------------------------------------------------- - */
        public void m_f__set_data(int parm_nSetInx,
                                       string parm_strName,
                                       string parm_strVal,
                                       string parm_strDelimiter)
        {
            m_straDataSet_req[parm_nSetInx, 1] +=
                (parm_strName + "=" + parm_strVal + parm_strDelimiter);
        }
        /* - ----------------------------------------------------------- - */
        public void m_f__set_data(int parm_nSetInx,
                                       string parm_strName,
                                       string parm_strVal)
        {
            m_f__set_data(parm_nSetInx, parm_strName, parm_strVal, "\x1f");
        }
        /* - ----------------------------------------------------------- - */
        public void m_f__add_data(int parm_nSetInx,
                                       int parm_nDataInx,
                                       string parm_strDelimiter)
        {
            m_straDataSet_req[parm_nSetInx, 1] +=
                (m_straDataSet_req[parm_nDataInx, 0] + "=" +
                  m_straDataSet_req[parm_nDataInx, 1] + parm_strDelimiter);
        }
        /* =============================================================== */

        /* =============================================================== */
        /* +    TX 실행                                                  + */
        /* - ----------------------------------------------------------- - */
        public void m_f__do_tx(string parm_strReqTx,
                                    string parm_strTxCD,
                                    int parm_nDataSetInx_req,
                                    string parm_strCustIP,
                                    string parm_strSiteCD,
                                    string parm_strSiteKey,
                                    string parm_strOrdrIDxx)
        {
            if (!m_strDataSet_enc_info.Equals(""))
            {
                m_c_Payplus.lf_PP_CLI_LIB__set_crypto_info(m_strDataSet_trace_no,
                    m_strDataSet_enc_info, m_strDataSet_enc);
                
                /* =============================================================== */
                /* 데이터 처리(ordr_no)    								   */
                /* =============================================================== */
                m_c_Payplus.lf_PP_CLI_LIB__set_plan_data(m_straDataSet_req[parm_nDataSetInx_req, 1]);
            }
            else
            {
                m_c_Payplus.lf_PP_CLI_LIB__set_plan_data(m_straDataSet_req[parm_nDataSetInx_req, 1]);
            }

            m_c_Payplus.lf_PP_CLI_LIB__do_tx(parm_strSiteCD, parm_strSiteKey,
                parm_strTxCD, parm_strCustIP, parm_strOrdrIDxx);

            m_strDataSet_res = m_c_Payplus.lf_PP_CLI_LIB__get_data();

            m_f__parse_res();
            m_strResCD = m_f__get_res("res_cd");
            m_strResMsg = m_f__get_res("res_msg");

        }
        /* =============================================================== */

        /* =============================================================== */
        /* +    응답 DATA 분석                                           + */
        /* - ----------------------------------------------------------- - */
        private void m_f__parse_res()
        {
            string[] straResDataList;
            string[] straResData;
            Int32 nInx;
            m_nResDataCnt = 0;
            straResDataList = m_strDataSet_res.Split('\x1f');
            m_nResDataCnt = straResDataList.Length;
            m_straResData = new string[m_nResDataCnt, 2];

            for (nInx = 0; nInx < m_nResDataCnt; nInx++)
            {
                straResData = straResDataList[nInx].Split('=');

                if (straResData[0].Equals(""))
                    break;

                m_straResData[nInx, 0] = straResData[0];
                m_straResData[nInx, 1] = straResData[1];
            }
        }
        /* =============================================================== */

        /* =============================================================== */
        /* +    응답 DATA                                                + */
        /* - ----------------------------------------------------------- - */
        public string m_f__get_res(string parm_strDataName)
        {
            string strRT;
            Int32 nInx;

            for (nInx = 0, strRT = ""; nInx < m_nResDataCnt; nInx++)
            {
                if (m_straResData[nInx, 0] == null)
                    break;

                if (m_straResData[nInx, 0].Equals(parm_strDataName))
                {
                    strRT = m_straResData[nInx, 1];
                    break;
                }
            }
            return strRT;
        }
        /* =============================================================== */
    }
}