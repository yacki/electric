Module PCI8735

    '#################### AD硬件参数PCI8735_PARA_AD定义 #####################
    ' 用于AD采样的实际硬件参数
    Structure PCI8735_PARA_AD

        Dim FirstChannel As Int32      ' 首通道[0, 31]
        Dim LastChannel As Int32       ' 末通道[0, 31],要求末通道必须大于或等于首通道
        Dim InputRange As Int32        ' 模拟量输入量程范围,其取值请见下面相关常量定义
        Dim GroundingMode As Int32     ' 接地方式(单端或双端选择),其取值请见下面相关常量定义
        Dim Gains As Long
    End Structure
    '***********************************************************
    ' AD硬件参数PCI8735_PARA_AD中的InputRange模拟量输入范围所使用的选项
    Public Const PCI8735_INPUT_N10000_P10000mV = &H0 ' ±10000mV
    Public Const PCI8735_INPUT_N5000_P5000mV = &H1   ' ±5000mV
    Public Const PCI8735_INPUT_N2500_P2500mV = &H2   ' ±2500mV
    Public Const PCI8735_INPUT_0_P10000mV = &H3      ' 0～10000mV

    '***********************************************************
    ' AD参数PCI8735_PARA_AD中的Gains使用的硬件增益选项
    Public Const PCI8735_GAINS_1MULT = &H0           ' 1倍增益
    Public Const PCI8735_GAINS_2MULT = &H1           ' 2倍增益
    Public Const PCI8735_GAINS_4MULT = &H2           ' 4倍增益
    Public Const PCI8735_GAINS_8MULT = &H3           ' 8倍增益

    '***********************************************************
    ' AD参数PCI8735_PARA_AD中的GroundingMode使用的模拟信号接地方式选项
    Public Const PCI8735_GNDMODE_SE = &H0            ' 单端方式(SE:Single end)
    Public Const PCI8735_GNDMODE_DI = &H1            ' 双端方式(DI:Differential)

    '***********************************************************
    ' CreateFileObject()所用的文件操作方式控制字(可通过或指令实现多种方式并操作)
    Public Const PCI8735_modeRead = &H0                ' 只读文件方式
    Public Const PCI8735_modeWrite = &H1               ' 只写文件方式
    Public Const PCI8735_modeReadWrite = &H2           ' 既读又写文件方式
    Public Const PCI8735_modeCreate = &H1000           ' 如果文件不存可以创建该文件，如果存在，则重建此文件，并清0
    Public Const PCI8735_typeText = &H4000             ' 以文本方式操作文件
    '######################## 设备对象管理函数 ##############################
    Declare Function PCI8735_CreateDevice Lib "PCI8735" (ByVal DeviceLgcID As Int32) As Int32 ' 用逻辑号创建设备对象
    Declare Function PCI8735_CreateDeviceEx Lib "PCI8735" (ByVal DevicePhysID As Int32) As Int32  ' 用物理号创建设备对象
    Declare Function PCI8735_GetDeviceCount Lib "PCI8735" (ByVal hDevice As IntPtr) As Int32    ' 取得设备总台数
    Declare Function PCI8735_GetDeviceCurrentID Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef DeviceLgcID As Int32, ByRef DevicePhysID As Int32) As Boolean ' 本卡物理ID号无效
    Declare Function PCI8735_ListDeviceDlg Lib "PCI8735" (ByVal hDevice As IntPtr) As Boolean ' 以对话框窗体方式列表系统当中的所有的该PCI设备
    Declare Function PCI8735_ReleaseDevice Lib "PCI8735" (ByVal hDevice As IntPtr) As Boolean ' 仅释放设备对象

    '####################### AD数据读取函数 #################################
    ' 适于大多数普通用户，这些接口最简单、最快捷、最可靠，让用户不必知道设备
    ' 低层复杂的硬件控制协议和繁多的软件控制编程，仅用下面的初始化设备和读取
    ' AD数据两个函数便能轻松高效地实现高速、连续的数据采集

    Declare Function PCI8735_InitDeviceAD Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef pADPara As PCI8735_PARA_AD) As Boolean ' 硬件参数, 它仅在此函数中决定硬件状态

    Declare Function PCI8735_ReadDeviceAD Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef ADBuffer As UInt16, ByVal nReadSizeWords As Int32, ByRef nRetSizeWords As Int32) As Boolean  ' 返回实际读取的长度(字)

    Declare Function PCI8735_ReleaseDeviceAD Lib "PCI8735" (ByVal hDevice As IntPtr) As Boolean ' 停止释放AD对象

    '##################### AD的硬件参数操作函数 ###########################
    Declare Function PCI8735_SaveParaAD Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef pADPara As PCI8735_PARA_AD) As Boolean     ' 将当前的AD采样参数保存至系统中
    Declare Function PCI8735_LoadParaAD Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef pADPara As PCI8735_PARA_AD) As Boolean     ' 将AD采样参数从系统中读出
    Declare Function PCI8735_ResetParaAD Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef pADPara As PCI8735_PARA_AD) As Boolean    ' 将AD采样参数恢复至出厂默认值

    '####################### 数字I/O输入输出函数 #################################
    ' 用户可以使用WriteRegisterULong和ReadRegisterULong等函数直接控制寄存器进行I/O
    ' 输入输出，但使用下面两个函数更省事，它不需要您关心寄存器分配和位操作等，而只
    ' 需象VB等语言的属性操作那么简单地实现各开关量通道的控制。
    Declare Function PCI8735_GetDeviceDI Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef bDISts As Byte) As Boolean     ' 开关输入状态(注意: 必须定义为16个字节元素的数组)

    Declare Function PCI8735_SetDeviceDO Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef bDOSts As Byte) As Boolean     ' 开关输出状态(注意: 必须定义为16个字节元素的数组)

    Declare Function PCI8735_RetDeviceDO Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef bDOSts As Byte) As Boolean     ' 获得开关输出状态(注意: 必须定义为16个字节元素的数组)

    '################# 内存映射寄存器直接操作及读写函数 ########################
    ' 适用于用户对本设备更直接、更特殊、更低层、更复杂的控制。比如根据特殊的
    ' 控制对象需要特殊的控制流程和控制效率时，则用户可以使用这些接口予以实现。
    Declare Function PCI8735_GetDeviceAddr Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef LinearAddr As UInt32, ByRef PhysAddr As UInt32, ByVal RegisterID As Int32) As Boolean            ' 设备寄存器组的ID号（0-5）

    Declare Function PCI8735_WriteRegisterByte Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal LinearAddr As UInt32, ByVal OffsetBytes As UInt32, ByVal Value As Byte) As Boolean          ' 往指定地址写入单字节数据（其地址由线性基地址和偏移位置决定）

    Declare Function PCI8735_WriteRegisterWord Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal LinearAddr As UInt32, ByVal OffsetBytes As UInt32, ByVal Value As Int32) As Boolean

    Declare Function PCI8735_WriteRegisterULong Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal LinearAddr As UInt32, ByVal OffsetBytes As UInt32, ByVal Value As UInt32) As Boolean

    Declare Function PCI8735_ReadRegisterByte Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal LinearAddr As UInt32, ByVal OffsetBytes As UInt32) As Byte


    Declare Function PCI8735_ReadRegisterWord Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal LinearAddr As UInt32, ByVal OffsetBytes As UInt32) As Int32


    Declare Function PCI8735_ReadRegisterULong Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal LinearAddr As UInt32, ByVal OffsetBytes As UInt32) As UInt32

    '################# I/O端口直接操作及读写函数 ########################
    ' 适用于用户对本设备更直接、更特殊、更低层、更复杂的控制。比如根据特殊的
    ' 控制对象需要特殊的控制流程和控制效率时，则用户可以使用这些接口予以实现。
    ' 但这些函数主要适用于传统设备，如ISA总线、并口、串口等设备，不能用于本PCI设备
    Declare Function PCI8735_WritePortByte Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal nPort As UInt32, ByVal Value As Byte) As Boolean         ' 写出的8位整型数据

    Declare Function PCI8735_WritePortWord Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal nPort As UInt32, ByVal Value As UInt16) As Boolean
    Declare Function PCI8735_WritePortULong Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal nPort As UInt32, ByVal Value As UInt32) As Boolean

    Declare Function PCI8735_ReadPortByte Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal nPort As UInt32) As Byte
    Declare Function PCI8735_ReadPortWord Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal nPort As UInt32) As UInt16
    Declare Function PCI8735_ReadPortULong Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal nPort As UInt32) As UInt32
    ' 如果您要在用户模式下，直接访问硬件端口，请安装并使用ISA\CommUser下的驱动ReadPortByteEx等函数

    '######################### 文件操作函数 ##############################
    Declare Function PCI8735_CreateFileObject Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal strFileName As String, ByVal Mode As Int32) As Int32
    Declare Function PCI8735_WriteFile Lib "PCI8735" (ByVal hFileObject As IntPtr, ByRef pDataBuffer As Int32, ByVal nWriteSizeBytes As Int32) As Boolean
    Declare Function PCI8735_ReadFile Lib "PCI8735" (ByVal hFileObject As IntPtr, ByRef pDataBuffer As Int32, ByVal nOffsetBytes As Int32, ByVal nReadSizeBytes As Int32) As Boolean
    Declare Function PCI8735_SetFileOffset Lib "PCI8735" (ByVal hFileObject As IntPtr, ByVal nOffsetBytes As Int32) As Boolean
    Declare Function PCI8735_GetFileLength Lib "PCI8735" (ByVal hFileObject As IntPtr) As UInt32        ' 取得指定文件长度（字节）
    Declare Function PCI8735_ReleaseFile Lib "PCI8735" (ByVal hFileObject As IntPtr) As Boolean       ' 获得指定盘符的磁盘空间(注意使用64位变量)
    Declare Function PCI8735_GetDiskFreeBytes Lib "PCI8735" (ByVal strDiskName As String) As UInt32   ' 盘符名,如C盘为"C:\\", D盘为"D:\\"

    '########################### 线程操作函数 ######################################
    Declare Function PCI8735_CreateSystemEvent Lib "PCI8735" (ByVal void) As Int32                     ' 创建内核事件对象，供VB子线程等函数使用
    Declare Function PCI8735_ReleaseSystemEvent Lib "PCI8735" (ByVal hEvent As IntPtr) As Boolean    ' 释放内核事件对象
    Declare Function PCI8735_CreateVBThread Lib "PCI8735" (ByRef hThread As IntPtr, ByVal RoutineAddr As Int32) As Boolean    ' 创建VB子线程
    Declare Function PCI8735_TerminateVBThread Lib "PCI8735" (ByVal hThread As IntPtr) As Boolean    ' 释放VB子线程
    Declare Function PCI8735_DelayTimeUs Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal nTimeUs As Int32) As Boolean           ' 微秒级延时函数

    '############################### 辅助函数 ###################################
    Declare Function PCI8735_kbhit Lib "PCI8735" (ByVal void) As Boolean ' 探测用户是否有击键动作(在控制台应用程序Console中且在非VC语言中)
    Declare Function PCI8735_getch Lib "PCI8735" (ByVal void) As Byte    ' 等待并获取用户击键值(在控制台应用程序Console中有效)

    Declare Function PCI8735_SaveParaInt Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal strParaName As String, ByVal nValue As Int32) As Boolean        ' 将整型数据保存到注册表中(Device-x\Others)
    Declare Function PCI8735_LoadParaInt Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal strParaName As String, ByVal nDefaultVal As Int32) As UInt32       ' 将整型数据从注册表中回读出来(Device-x\Others)
    Declare Function PCI8735_SaveParaString Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal strParaName As String, ByVal strParaVal As String) As Boolean  ' 将字符串数据保存到注册表中(Device-x\Others)
    Declare Function PCI8735_LoadParaString Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal strParaName As String, ByVal strParaVal As String, ByVal strDefaultVal As String) As Boolean ' 将字符串数据从注册表中回读出来(Device-x\Others)

    Declare Function PCI8735_GetLastErrorEx Lib "PCI8735" (ByVal strFuncName As String, ByVal strErrorMsg As String) As Int32






End Module
