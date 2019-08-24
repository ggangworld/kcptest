<%@ Page Language="C#" AutoEventWireup="true" CodeFile="./pp_cli_hub.aspx.cs" Inherits="KCP.PP_CLI_COM.SRC.pp_cli_com" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>*** NHN KCP Online Payment System 7.0[AX_HUB Version] ***</title>

    <script type="text/javascript">
            function goResult()
            {
               document.pay_info.submit();
            }

            // 결제 중 새로고침 방지 샘플 스크립트
            function noRefresh()
            {
                /* CTRL + N키 막음. */
                if ((event.keyCode == 78) && (event.ctrlKey == true))
                {
                    event.keyCode = 0;
                    return false;
                }
                /* F5 번키 막음. */
                if(event.keyCode == 116)
                {
                    event.keyCode = 0;
                    return false;
                }
            }
            document.onkeydown = noRefresh ;

    </script>
    
    
</head>
<body onload="goResult();">
    <form name="pay_info" method="post" action="./result.aspx">
        <input type="hidden" name="site_cd"         value="<%=m_strCFG_site_cd%>">  <!-- 사이트코드 -->
        <input type="hidden" name="req_tx"          value="<%=req_tx%>">            <!-- 요청 구분 -->
        <input type="hidden" name="use_pay_method"  value="<%=use_pay_method%>">    <!-- 사용한 결제 수단 -->
        <input type="hidden" name="bSucc"           value="<%=bSucc%>">             <!-- 쇼핑몰 DB 처리 성공 여부 -->
        <input type="hidden" name="res_cd"          value="<%=res_cd%>">            <!-- 결과 코드 -->
        <input type="hidden" name="res_msg"         value="<%=res_msg%>">           <!-- 결과 메세지 -->
        <input type="hidden" name="ordr_idxx"       value="<%=ordr_idxx%>">         <!-- 주문번호 -->
        <input type="hidden" name="tno"             value="<%=tno%>">               <!-- KCP 거래번호 -->
        <input type="hidden" name="good_name"       value="<%=good_name%>">         <!-- 상품명 -->
        <input type="hidden" name="buyr_name"       value="<%=buyr_name%>">         <!-- 주문자명 -->
        <input type="hidden" name="buyr_tel1"       value="<%=buyr_tel1%>">         <!-- 주문자 전화번호 -->
        <input type="hidden" name="buyr_tel2"       value="<%=buyr_tel2%>">         <!-- 주문자 휴대폰번호 -->
        <input type="hidden" name="buyr_mail"       value="<%=buyr_mail%>">         <!-- 주문자 E-mail -->
        <input type="hidden" name="app_time"        value="<%=app_time%>">          <!-- 승인시간 -->
        <input type="hidden" name="amount"          value="<%=amount%>">            <!-- KCP 실제 거래 금액 -->
        
        <!-- 신용카드 정보 -->
        <input type="hidden" name="card_cd"         value="<%=card_cd%>">           <!-- 카드코드 -->
        <input type="hidden" name="card_name"       value="<%=card_name%>">         <!-- 카드이름 -->
        <input type="hidden" name="app_no"          value="<%=app_no%>">            <!-- 승인번호 -->
        <input type="hidden" name="noinf"           value="<%=noinf%>">             <!-- 무이자여부 -->
        <input type="hidden" name="quota"           value="<%=quota%>">             <!-- 할부개월 -->
        <input type="hidden" name="partcanc_yn"     value="<%=partcanc_yn%>">       <!-- 부분취소가능유무 -->
        <input type="hidden" name="card_bin_type_01" value="<%=card_bin_type_01%>"> <!-- 카드구분1 -->
        <input type="hidden" name="card_bin_type_02" value="<%=card_bin_type_02%>"> <!-- 카드구분2 -->
        <!-- 계좌이체 정보 -->
        <input type="hidden" name="bank_name"       value="<%=bank_name%>">         <!-- 은행명 -->
        <input type="hidden" name="bank_code"       value="<%=bank_code%>">         <!-- 은행코드 -->
        <!-- 가상계좌 정보 -->
        <input type="hidden" name="bankname"        value="<%=bankname%>">          <!-- 입금 은행 -->
        <input type="hidden" name="depositor"       value="<%=depositor%>">         <!-- 입금계좌 예금주 -->
        <input type="hidden" name="account"         value="<%=account%>">           <!-- 입금계좌 번호 -->
        <input type="hidden" name="va_date"         value="<%=va_date%>">           <!-- 가상계좌 입금마감시간 -->
        <!-- 포인트 정보 -->
        <input type="hidden" name="pnt_issue"       value="<%=pnt_issue%>">         <!-- 포인트 서비스사 -->
        <input type="hidden" name="pnt_app_time"    value="<%=pnt_app_time%>">      <!-- 승인시간 -->
        <input type="hidden" name="pnt_app_no"      value="<%=pnt_app_no%>">        <!-- 승인번호 -->
        <input type="hidden" name="pnt_amount"      value="<%=pnt_amount%>">        <!-- 적립금액 or 사용금액 -->
        <input type="hidden" name="add_pnt"         value="<%=add_pnt%>">           <!-- 발생 포인트 -->
        <input type="hidden" name="use_pnt"         value="<%=use_pnt%>">           <!-- 사용가능 포인트 -->
        <input type="hidden" name="rsv_pnt"         value="<%=rsv_pnt%>">           <!-- 총 누적 포인트 -->
        <!-- 휴대폰 정보 -->
        <input type="hidden" name="commid"          value="<%=commid%>">            <!-- 통신사 코드 -->
        <input type="hidden" name="mobile_no"       value="<%=mobile_no%>">         <!-- 휴대폰 번호 -->
        <!-- 상품권 정보 -->
        <input type="hidden" name="tk_van_code"     value="<%=tk_van_code%>">       <!-- 발급사 코드 -->
        <input type="hidden" name="tk_app_no"       value="<%=tk_app_no%>">         <!-- 승인 번호 -->
        <!-- 현금영수증 정보 -->
        <input type="hidden" name="cash_yn"         value="<%=cash_yn%>">           <!-- 현금영수증 등록 여부 -->
        <input type="hidden" name="cash_authno"     value="<%=cash_authno%>">       <!-- 현금 영수증 승인 번호 -->
        <input type="hidden" name="cash_tr_code"    value="<%=cash_tr_code%>">      <!-- 현금 영수증 발행 구분 -->
        <input type="hidden" name="cash_id_info"    value="<%=cash_id_info%>">      <!-- 현금 영수증 등록 번호 -->
        <input type="hidden" name="cash_no"         value="<%=cash_no%>">           <!-- 현금 영수증 거래 번호 -->
        
        
    </form>
</body>
</html>
           