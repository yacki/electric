Module PCI8735

    '#################### ADӲ������PCI8735_PARA_AD���� #####################
    ' ����AD������ʵ��Ӳ������
    Structure PCI8735_PARA_AD

        Dim FirstChannel As Int32      ' ��ͨ��[0, 31]
        Dim LastChannel As Int32       ' ĩͨ��[0, 31],Ҫ��ĩͨ��������ڻ������ͨ��
        Dim InputRange As Int32        ' ģ�����������̷�Χ,��ȡֵ���������س�������
        Dim GroundingMode As Int32     ' �ӵط�ʽ(���˻�˫��ѡ��),��ȡֵ���������س�������
        Dim Gains As Long
    End Structure
    '***********************************************************
    ' ADӲ������PCI8735_PARA_AD�е�InputRangeģ�������뷶Χ��ʹ�õ�ѡ��
    Public Const PCI8735_INPUT_N10000_P10000mV = &H0 ' ��10000mV
    Public Const PCI8735_INPUT_N5000_P5000mV = &H1   ' ��5000mV
    Public Const PCI8735_INPUT_N2500_P2500mV = &H2   ' ��2500mV
    Public Const PCI8735_INPUT_0_P10000mV = &H3      ' 0��10000mV

    '***********************************************************
    ' AD����PCI8735_PARA_AD�е�Gainsʹ�õ�Ӳ������ѡ��
    Public Const PCI8735_GAINS_1MULT = &H0           ' 1������
    Public Const PCI8735_GAINS_2MULT = &H1           ' 2������
    Public Const PCI8735_GAINS_4MULT = &H2           ' 4������
    Public Const PCI8735_GAINS_8MULT = &H3           ' 8������

    '***********************************************************
    ' AD����PCI8735_PARA_AD�е�GroundingModeʹ�õ�ģ���źŽӵط�ʽѡ��
    Public Const PCI8735_GNDMODE_SE = &H0            ' ���˷�ʽ(SE:Single end)
    Public Const PCI8735_GNDMODE_DI = &H1            ' ˫�˷�ʽ(DI:Differential)

    '***********************************************************
    ' CreateFileObject()���õ��ļ�������ʽ������(��ͨ����ָ��ʵ�ֶ��ַ�ʽ������)
    Public Const PCI8735_modeRead = &H0                ' ֻ���ļ���ʽ
    Public Const PCI8735_modeWrite = &H1               ' ֻд�ļ���ʽ
    Public Const PCI8735_modeReadWrite = &H2           ' �ȶ���д�ļ���ʽ
    Public Const PCI8735_modeCreate = &H1000           ' ����ļ�������Դ������ļ���������ڣ����ؽ����ļ�������0
    Public Const PCI8735_typeText = &H4000             ' ���ı���ʽ�����ļ�
    '######################## �豸��������� ##############################
    Declare Function PCI8735_CreateDevice Lib "PCI8735" (ByVal DeviceLgcID As Int32) As Int32 ' ���߼��Ŵ����豸����
    Declare Function PCI8735_CreateDeviceEx Lib "PCI8735" (ByVal DevicePhysID As Int32) As Int32  ' ������Ŵ����豸����
    Declare Function PCI8735_GetDeviceCount Lib "PCI8735" (ByVal hDevice As IntPtr) As Int32    ' ȡ���豸��̨��
    Declare Function PCI8735_GetDeviceCurrentID Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef DeviceLgcID As Int32, ByRef DevicePhysID As Int32) As Boolean ' ��������ID����Ч
    Declare Function PCI8735_ListDeviceDlg Lib "PCI8735" (ByVal hDevice As IntPtr) As Boolean ' �ԶԻ����巽ʽ�б�ϵͳ���е����еĸ�PCI�豸
    Declare Function PCI8735_ReleaseDevice Lib "PCI8735" (ByVal hDevice As IntPtr) As Boolean ' ���ͷ��豸����

    '####################### AD���ݶ�ȡ���� #################################
    ' ���ڴ������ͨ�û�����Щ�ӿ���򵥡����ݡ���ɿ������û�����֪���豸
    ' �Ͳ㸴�ӵ�Ӳ������Э��ͷ����������Ʊ�̣���������ĳ�ʼ���豸�Ͷ�ȡ
    ' AD�������������������ɸ�Ч��ʵ�ָ��١����������ݲɼ�

    Declare Function PCI8735_InitDeviceAD Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef pADPara As PCI8735_PARA_AD) As Boolean ' Ӳ������, �����ڴ˺����о���Ӳ��״̬

    Declare Function PCI8735_ReadDeviceAD Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef ADBuffer As UInt16, ByVal nReadSizeWords As Int32, ByRef nRetSizeWords As Int32) As Boolean  ' ����ʵ�ʶ�ȡ�ĳ���(��)

    Declare Function PCI8735_ReleaseDeviceAD Lib "PCI8735" (ByVal hDevice As IntPtr) As Boolean ' ֹͣ�ͷ�AD����

    '##################### AD��Ӳ�������������� ###########################
    Declare Function PCI8735_SaveParaAD Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef pADPara As PCI8735_PARA_AD) As Boolean     ' ����ǰ��AD��������������ϵͳ��
    Declare Function PCI8735_LoadParaAD Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef pADPara As PCI8735_PARA_AD) As Boolean     ' ��AD����������ϵͳ�ж���
    Declare Function PCI8735_ResetParaAD Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef pADPara As PCI8735_PARA_AD) As Boolean    ' ��AD���������ָ�������Ĭ��ֵ

    '####################### ����I/O����������� #################################
    ' �û�����ʹ��WriteRegisterULong��ReadRegisterULong�Ⱥ���ֱ�ӿ��ƼĴ�������I/O
    ' �����������ʹ����������������ʡ�£�������Ҫ�����ļĴ��������λ�����ȣ���ֻ
    ' ����VB�����Ե����Բ�����ô�򵥵�ʵ�ָ�������ͨ���Ŀ��ơ�
    Declare Function PCI8735_GetDeviceDI Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef bDISts As Byte) As Boolean     ' ��������״̬(ע��: ���붨��Ϊ16���ֽ�Ԫ�ص�����)

    Declare Function PCI8735_SetDeviceDO Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef bDOSts As Byte) As Boolean     ' �������״̬(ע��: ���붨��Ϊ16���ֽ�Ԫ�ص�����)

    Declare Function PCI8735_RetDeviceDO Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef bDOSts As Byte) As Boolean     ' ��ÿ������״̬(ע��: ���붨��Ϊ16���ֽ�Ԫ�ص�����)

    '################# �ڴ�ӳ��Ĵ���ֱ�Ӳ�������д���� ########################
    ' �������û��Ա��豸��ֱ�ӡ������⡢���Ͳ㡢�����ӵĿ��ơ�������������
    ' ���ƶ�����Ҫ����Ŀ������̺Ϳ���Ч��ʱ�����û�����ʹ����Щ�ӿ�����ʵ�֡�
    Declare Function PCI8735_GetDeviceAddr Lib "PCI8735" (ByVal hDevice As IntPtr, ByRef LinearAddr As UInt32, ByRef PhysAddr As UInt32, ByVal RegisterID As Int32) As Boolean            ' �豸�Ĵ������ID�ţ�0-5��

    Declare Function PCI8735_WriteRegisterByte Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal LinearAddr As UInt32, ByVal OffsetBytes As UInt32, ByVal Value As Byte) As Boolean          ' ��ָ����ַд�뵥�ֽ����ݣ����ַ�����Ի���ַ��ƫ��λ�þ�����

    Declare Function PCI8735_WriteRegisterWord Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal LinearAddr As UInt32, ByVal OffsetBytes As UInt32, ByVal Value As Int32) As Boolean

    Declare Function PCI8735_WriteRegisterULong Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal LinearAddr As UInt32, ByVal OffsetBytes As UInt32, ByVal Value As UInt32) As Boolean

    Declare Function PCI8735_ReadRegisterByte Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal LinearAddr As UInt32, ByVal OffsetBytes As UInt32) As Byte


    Declare Function PCI8735_ReadRegisterWord Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal LinearAddr As UInt32, ByVal OffsetBytes As UInt32) As Int32


    Declare Function PCI8735_ReadRegisterULong Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal LinearAddr As UInt32, ByVal OffsetBytes As UInt32) As UInt32

    '################# I/O�˿�ֱ�Ӳ�������д���� ########################
    ' �������û��Ա��豸��ֱ�ӡ������⡢���Ͳ㡢�����ӵĿ��ơ�������������
    ' ���ƶ�����Ҫ����Ŀ������̺Ϳ���Ч��ʱ�����û�����ʹ����Щ�ӿ�����ʵ�֡�
    ' ����Щ������Ҫ�����ڴ�ͳ�豸����ISA���ߡ����ڡ����ڵ��豸���������ڱ�PCI�豸
    Declare Function PCI8735_WritePortByte Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal nPort As UInt32, ByVal Value As Byte) As Boolean         ' д����8λ��������

    Declare Function PCI8735_WritePortWord Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal nPort As UInt32, ByVal Value As UInt16) As Boolean
    Declare Function PCI8735_WritePortULong Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal nPort As UInt32, ByVal Value As UInt32) As Boolean

    Declare Function PCI8735_ReadPortByte Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal nPort As UInt32) As Byte
    Declare Function PCI8735_ReadPortWord Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal nPort As UInt32) As UInt16
    Declare Function PCI8735_ReadPortULong Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal nPort As UInt32) As UInt32
    ' �����Ҫ���û�ģʽ�£�ֱ�ӷ���Ӳ���˿ڣ��밲װ��ʹ��ISA\CommUser�µ�����ReadPortByteEx�Ⱥ���

    '######################### �ļ��������� ##############################
    Declare Function PCI8735_CreateFileObject Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal strFileName As String, ByVal Mode As Int32) As Int32
    Declare Function PCI8735_WriteFile Lib "PCI8735" (ByVal hFileObject As IntPtr, ByRef pDataBuffer As Int32, ByVal nWriteSizeBytes As Int32) As Boolean
    Declare Function PCI8735_ReadFile Lib "PCI8735" (ByVal hFileObject As IntPtr, ByRef pDataBuffer As Int32, ByVal nOffsetBytes As Int32, ByVal nReadSizeBytes As Int32) As Boolean
    Declare Function PCI8735_SetFileOffset Lib "PCI8735" (ByVal hFileObject As IntPtr, ByVal nOffsetBytes As Int32) As Boolean
    Declare Function PCI8735_GetFileLength Lib "PCI8735" (ByVal hFileObject As IntPtr) As UInt32        ' ȡ��ָ���ļ����ȣ��ֽڣ�
    Declare Function PCI8735_ReleaseFile Lib "PCI8735" (ByVal hFileObject As IntPtr) As Boolean       ' ���ָ���̷��Ĵ��̿ռ�(ע��ʹ��64λ����)
    Declare Function PCI8735_GetDiskFreeBytes Lib "PCI8735" (ByVal strDiskName As String) As UInt32   ' �̷���,��C��Ϊ"C:\\", D��Ϊ"D:\\"

    '########################### �̲߳������� ######################################
    Declare Function PCI8735_CreateSystemEvent Lib "PCI8735" (ByVal void) As Int32                     ' �����ں��¼����󣬹�VB���̵߳Ⱥ���ʹ��
    Declare Function PCI8735_ReleaseSystemEvent Lib "PCI8735" (ByVal hEvent As IntPtr) As Boolean    ' �ͷ��ں��¼�����
    Declare Function PCI8735_CreateVBThread Lib "PCI8735" (ByRef hThread As IntPtr, ByVal RoutineAddr As Int32) As Boolean    ' ����VB���߳�
    Declare Function PCI8735_TerminateVBThread Lib "PCI8735" (ByVal hThread As IntPtr) As Boolean    ' �ͷ�VB���߳�
    Declare Function PCI8735_DelayTimeUs Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal nTimeUs As Int32) As Boolean           ' ΢�뼶��ʱ����

    '############################### �������� ###################################
    Declare Function PCI8735_kbhit Lib "PCI8735" (ByVal void) As Boolean ' ̽���û��Ƿ��л�������(�ڿ���̨Ӧ�ó���Console�����ڷ�VC������)
    Declare Function PCI8735_getch Lib "PCI8735" (ByVal void) As Byte    ' �ȴ�����ȡ�û�����ֵ(�ڿ���̨Ӧ�ó���Console����Ч)

    Declare Function PCI8735_SaveParaInt Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal strParaName As String, ByVal nValue As Int32) As Boolean        ' ���������ݱ��浽ע�����(Device-x\Others)
    Declare Function PCI8735_LoadParaInt Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal strParaName As String, ByVal nDefaultVal As Int32) As UInt32       ' ���������ݴ�ע����лض�����(Device-x\Others)
    Declare Function PCI8735_SaveParaString Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal strParaName As String, ByVal strParaVal As String) As Boolean  ' ���ַ������ݱ��浽ע�����(Device-x\Others)
    Declare Function PCI8735_LoadParaString Lib "PCI8735" (ByVal hDevice As IntPtr, ByVal strParaName As String, ByVal strParaVal As String, ByVal strDefaultVal As String) As Boolean ' ���ַ������ݴ�ע����лض�����(Device-x\Others)

    Declare Function PCI8735_GetLastErrorEx Lib "PCI8735" (ByVal strFuncName As String, ByVal strErrorMsg As String) As Int32






End Module
