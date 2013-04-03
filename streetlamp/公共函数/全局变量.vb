Module 全局变量

    '*****************常量*******************************************
    Public MAP_MAX_SIZE As Integer = 10   '地图的滑动条的最大值
    Public MAP_MID_SIZE As Integer = 5    '地图的滑动条的常值
    Public MAP_SIZE_BASE As Double = 0.5   '缩放的大小基数
    Public MAP_SIZE_CHANGE As Double = 0.05    '缩放的比例
    Public PROJECT_TITLE_STRING As String = "苏州电网故障报警系统"
    Public Const LAMP_WIDE As Integer = 4
    Public Const LAMP_HEIGHT As Integer = 4
    Public Const MSG_ERROR_STRING As String = "数据库查询出错"
    Public Const LAMP_STATE_PROBLEM_ON As String = "异常灭灯"
    Public Const LAMP_STATE_PROBLEM_OFF As String = "异常亮灯"
    Public Const LAMP_STATE_ON As String = "亮"
    Public Const LAMP_STATE_OFF As String = "暗"
    Public Const LAMP_STATE_NORETURN As String = "无信息返回"
    Public Const LAMP_STATE_CONTROL As String = "控制状态"
    Public Const COMPANY_NAME As String = ""  '各种报表中的单位名称
    Public Const SYSTEM_VISION As Integer = 2 '软件版本，1为主控箱编号1字节，主控箱编号2为2字节
    Public Const LAMP_ID_LEN As Integer = 5  '3  终端编号中，灯的独立编号长度,扩展后为5位长度
    Public Const LAMP_ID_MAX As Integer = 20000  '999 灯的独立编号最大为20000 十六进制为4E20 1-10类型停止使用，类型号使用11-31类型
    Public Const TYPE_ID_MIN As Integer = 1 '除了0类型的最小的类型编号
    Public Const ALARMTYPE_OK As String = "吸合"  '开关量报警标志0
    Public Const ALARMTYPE_NOTOK As String = "断开"  '开关量报警标志1
    Public Const CONTROL_BOX_TYPE1_FLAG As Integer = 0  '旧版主控箱的命令标志
    Public Const CONTROL_BOX_TYPE2_FLAG As Integer = 10  '新版主控箱的命令标志
    Public Const POWERTYPE_BUTTERY As String = "电池"
    Public Const POWERTYPE_CURRENT As String = "交流电"
    Public Const CURRENT_ADTYPE As Integer = 1 '电流的AD值类型，1表示1个字节，2表示2个字节
    Public Const CONTROL_WAIT_TIME As Integer = 20  '最长等待时间为20秒
    Public Const OLD_DATALENGHT As Integer = 57  '传统三遥数据长度
    Public Const SMALL_DATALENGHT As Integer = 33 '小三遥数据长度
    Public Const HUILU_PROBLEM_INF As String = "回路开路"
    Public Const AUTO_ZHAOCETIME As Integer = 20  '每次操作完毕后等待40秒后自动招测


    '*****************地图相关变量*****************************************
    Public g_movepictag As Integer                ' 移动图片的标志
    Public g_beginpoint, g_endpoint As New Point  '移动的起始位置和终点位置
    Public g_midpoint As New Point                '地图的中点坐标,目的是为了缩放地图是保持图片在当前视窗中心缩放
    Public g_changemapvalue As Integer = MAP_MID_SIZE  '赋值map的大小值，即滑动条的值
    Public g_mapsizevalue As System.Drawing.Size   '地图的长、宽缩放值
    Public g_choosemapid As Integer  '被选中的地图编号
    Public g_mapname As String  '地图名称


    '******************绘制终端*********************************************
    Public g_fullcolor, g_closecolor, g_problemcolor, g_noreturncolor, g_partcolor As System.Drawing.Color  '终端系统的颜色
    Public g_lampmap As Graphics    '绘制终端
    Public g_lampdrawtag As Boolean = False   '使用graphic 的共享变量

    '******控制条件，即以电阻还是电流来判断终端开关的状态*******************
    Public g_controlcondition As String  '控制条件
    Public g_controlvaluepart, g_controlvalueall As Integer '控制值
    Public g_startid As Integer = 1   '1/3开的起始编号
    Public g_addboxtag As Integer  '增加主控箱的标志
    Public g_lampnum As Integer  '灯杆对应灯头个数
    'Public g_bianbivalue As Integer  '变比参数设置

    '*************************查询定位*************************************
    Public g_addstreettag As Integer  '增加查询区域的标志


    '********************时段控制******************************************
    Public g_divname() As String  '时段模式的名称
    Public g_specialdivname() As String  '特殊时段模式的名称
    Public g_sethuilutag As Boolean = False   '是否有当前的时段操作，

    '***********************用户权限**************************************
    Public g_rightmanage As String  '权限
    Public g_username As String '用户名
    Public g_password As String '密码


    '*********************公共函数****************************************
    Public g_controlboxid As String  '电控箱编号
    Public g_lampidstring As String  '终端编号
    Public g_shortlampid As String  '终端的三位独立编号
    Public g_dianzuad, g_currentad, g_information, g_presuread, g_powerad As Integer   '十六进制转换为十进制后三位字段,电阻，电流，备注
    Public g_controlboxname, g_lamptype '电控箱名称，景观灯类型，景观灯编号
    Public g_currentvalue, g_presurevalue, g_powervalue, g_yinshuvalue As Double '电流值及电压值,功率，功率因数
    Public g_dengzhuid, g_dengzhulampid As String  '灯杆的编号及每盏灯杆上灯的编号
    Public g_startidvalue As Integer  '1/3开的起始编号
    Public g_alarminf As New ArrayList  '报警信息
    Public g_sanyaopresure, g_sanyaocurrent, g_sanyaopower As Integer
    Public g_windowclose As Integer = 0  '用来判断是否已经开启子窗体，解决重复打开子窗体
    '*********************** 记录单灯的报警窗口是否弹出***********************
    Public g_lampalarm_tag As Boolean = False
    Public g_lampalarm_show As Boolean = False
    '**********************主控窗口对象**********************************
    Public g_welcomewinobj As New welcome_win  '主控界面
    '**********************系统设置参数**********************************
    Public g_sysjibiecontrol As String  '控制级别参数
    Public g_modgroup() As String  '模式组合\
   
    Public g_modtag As Integer  '选择下放的类型，1表示自动模式下放，2表示发送模拟量配置， 3表示发送开关量数据， 4表示发送测量板个数
    Public g_ycwaittime As Integer '遥测等待时间
    Public g_ycjgtime As Integer '召测间隔时间
    Public g_autozhaoce As Integer  '控制完毕后自动招测的等待时间，等待1分钟
    Public g_zhaocetag As Boolean = False  '是否需要开始准备招测



    '****************************模式下放*******************************
    Public g_mod_controlboxname As String '记录模式及时间等下放的时候选择的主控箱的名称
    ' Public g_detime As Integer = 3 '自动上传报警的延时时间
    '**************************光照度*********************************
    Public g_lightvalue As Double  '记录采集的光照度
    Public g_openvalue As Boolean = False '打开光照采集
    Public g_lightvalueset As Double  '记录光照度的阈值

    '*******************自动抄表*********************************
    Public g_checkmonth As Integer '按月查询
    Public g_checktime As Integer '按日查询
    Public g_chaobiaodate As Integer '抄表的日期 =-1表示按每天抄表
    Public g_chaobiaotime As Integer '抄表的时间
    Public g_chaobiaotag As Boolean = False '默认情况下关闭抄表
    Public g_getlamp_time As Integer = 0
    Public g_packtype As Integer = 0
    Public g_textbox_time_value As Integer = 0
    Public g_clearmod As Boolean = False
    Public g_lampwaittime As Integer = 0  '单灯招测
    Public g_lamptimes As Integer = streetlamp.My.MySettings.Default.PerTime  '单灯招测
    Public g_lampAlarmDo As String = streetlamp.My.MySettings.Default.AlarmDo  '单灯是否召测
    Public g_lampStarTime As Integer = streetlamp.My.MySettings.Default.StarTime
    Public g_lampEndTime As Integer = streetlamp.My.MySettings.Default.EndTime

    Public g_autotime As Integer '连着三次招测的次数
    



    Public Enum HG_TYPE
        '******************已使用的协议******************************
        HG_SET_MN_GONFIG = &H8B  '模拟量配置
        HG_GET_MN_CONFIG = &H8C  '回复模拟量配置
        HG_SET_TESTBOARDNUM = &H89 '配置测量板个数
        HG_GET_TESTBOARDNUM = &H8A '回复测量板个数配置
        HG_SET_KG_CONFIG = &H8D '配置开关量的跳变报警
        HG_GET_KG_CONFIG = &H8E  '回复开关量的跳变报警
        HG_SET_DIVMOD_CONFIG = &H1  '配置时控模式
        HG_GET_DIVMOD_CONFIG = &H3  '回复时控模式
        HG_SET_TIME_CONFIG = &H2 '校时配置
        HG_GET_TIME_CONFIG = &H4  '回复校时
        HG_HOST_LIGHT_AUTO = &H20  '!<单灯状态自动上传
        HG_HOST_SANYAO_AUTO = &H21     '!<大三遥数据上传
        HG_HOST_SAMALL_SANYAO_AUTO = &H25  '小三遥自动上传
        HG_HOST_SW_AUTO = &H22              '!<开关量自动上传
        HG_HOST_NEWSAYAO_AUTO = &H23  '具有招测功能的三遥自动上传
        HG_HOST_ALARMAUTO = &H24   '大三遥主控箱自动报警
        HG_HOST_SMALL_ALARMAUTO = &H26 '小三遥自动报警
        HG_SET_CONTROL_BOX_ID = &H81  '路段号配置
        HG_AUTO_CLOSE_ORDER = &H100  '淄博特殊要求手工控制后隔一段时间后要自动给关闭
        HG_ASK_POWERDATA97 = &H97  '97规约,询问数据
        HG_ACK_POWERDATA97 = &H98  '97规约 ，返回数据
        HG_ASK_POWERDATA07 = &H99  '07规约，询问数据
        HG_ACK_POWERDATA07 = &H9A '07规约，返回数据
        HG_SET_YEARCONFIG = &H9B  '年配置
        HG_GET_YEARCONFIG = &H9C  '年配置回复
        HG_ASK_TIMECONTROL = &HFB  '寻求时控信息
        HG_ACK_TIMECONTROL = &HFC  '应答时控
        HG_ASK_ROADID = &HF9 '寻求路段
        HG_ACK_ROADID = &HFA  '应答路段
        HG_ASK_TIME = &HF7  '寻求时间
        HG_ACK_TIME = &HF8 '应答时间



        ''配置路段号的格式
        '0+两个字节路段号
        HG_GET_CONTROL_BOX_ID = &H82  '回复路段号配置

        HG_GET_CONTROL_OPEN_CLOSE = &H11 '单灯控制的状态回复
        HG_SET_YAOCE = &H95  '招测发送命令
        HG_GET_YAOCE = &H96   '招测返回命令
        HG_SET_KAIGUAN = &H91  '招测开关量
        HG_GET_KAIGUAN = &H92  '招测开关量返回
        HG_GET_SMALLYAOCE = &H9D  '小三遥招测数据
        HG_SET_WAITTIME = &H93  '配置上传时间
        HG_GET_WAITIME = &H94  '返回上传配置状态

        '*************************************************************


        HG_SVR_LIGHT_CMD = &H10  '!<单灯命令
        HG_HOST_LIGHT_STATE
        HG_SVR_CONF = &H80    '!<配置命令
        HG_SVR_CONF_INTER = &H81 '!<区间配置命令
        HG_HOST_CONF_INTER_RES
        HG_SVR_CONF_TIME = &H83 '!<时间配置命令
        HG_SVR_CONF_TIME_RES
        HG_SVR_TIMECONTROL = &H85 '!<时控配置命令
        HG_HOST_TIMECONTROL_RES
        HG_SVR_RARIO = &H87 '!<互感器变比
        HG_HOST_RARIO_RES
        HG_SVR_SY_NUM = &H89 '!<三遥组号
        HG_HOST_SY_NUM_RES

    End Enum



End Module
