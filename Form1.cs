using Google.Protobuf;
using HOTEL.HotelManagement;
using HotelManagement;
using Mysqlx.Crud;
using Sunny.UI;
using Sunny.UI.Win32;
using System.Data;
using System.Windows.Forms;

namespace HOTEL
{
    public partial class Form1 : Form
    {
        IOrderRepository orderRepository = new OrderRepository();
        IRepository repository = new RoomRepository();
        ICheckInRepository checkInRepository = new CheckInRepository();
        List<RoomService> roomlist;
        public Form1()
        {
            InitializeComponent();
            // �������
            
           
            try
            {
                roomlist = repository.GetAllRoomServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ȡ�����������ʱ���ִ���: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DisplayRoomServices();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        static int DateDifference(DateTime date1, DateTime date2)
        {
            TimeSpan difference = date2 - date1;
            if (difference.TotalDays < 0)
            {
                difference = date1 - date2;
            }
            return difference.Days;
        }
    
        private void uiButton1_Click(object sender, EventArgs e)
        {
            Order order = new Order();
            CheckIn checkIn = new CheckIn();
            Customer customer = new Customer();
            RoomUseTime roomUseTime = new RoomUseTime();
            ICustomerRepository customerRepository = new CustomerRepository();
            IRoomUseTimeRepository roomUseTimeRepository = new RoomUseTimeRepository();
            // ���splitContainer1.Panel2�е����пؼ�
            splitContainer1.Panel2.Controls.Clear();

            // ����һ����񲼾����
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.RowCount = 18; // ����һ��
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

            // ����ID
            Label labelGuestIdOrder = new Label();
            labelGuestIdOrder.Text = "����ID��";
            labelGuestIdOrder.AutoSize = true;
            labelGuestIdOrder.Margin = new Padding(3, 5, 0, 5); // �������Marginֵ��ʹ�ı����������ǩ
            TextBox textBoxGuestIdOrder = new TextBox();
            textBoxGuestIdOrder.Margin = new Padding(3, 5, 5, 5);
            textBoxGuestIdOrder.Width = 150; // �����ı����ȣ�ʹ�����

            // �ͻ���ϵ��ʽ
            Label labelCustomerContact = new Label();
            labelCustomerContact.Text = "�ͻ���ϵ��ʽ��";
            labelCustomerContact.AutoSize = true;
            labelCustomerContact.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxCustomerContact = new TextBox();
            textBoxCustomerContact.Margin = new Padding(3, 5, 5, 5);
            textBoxCustomerContact.Width = 150;

            // ��ݱ�ʶ
            Label labelIdSh = new Label();
            labelIdSh.Text = "��ݱ�ʶ��";
            labelIdSh.AutoSize = true;
            labelIdSh.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxIdSh = new TextBox();
            textBoxIdSh.Margin = new Padding(3, 5, 5, 5);
            textBoxIdSh.Width = 150;

            // ����ID
            Label labelOrderId = new Label();
            labelOrderId.Text = "����ID��";
            labelOrderId.AutoSize = true;
            labelOrderId.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxOrderId = new TextBox();
            textBoxOrderId.Margin = new Padding(3, 5, 5, 5);
            textBoxOrderId.Width = 150;

            // ����ID
            Label labelRoomId = new Label();
            labelRoomId.Text = "����ID��";
            labelRoomId.AutoSize = true;
            labelRoomId.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxRoomId = new TextBox();
            textBoxRoomId.Margin = new Padding(3, 5, 5, 5);
            textBoxRoomId.Width = 150;

            // ��������
            Label labelOrderDate = new Label();
            labelOrderDate.Text = "�������ڣ�";
            labelOrderDate.AutoSize = true;
            labelOrderDate.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxOrderDate = new TextBox();
            textBoxOrderDate.Margin = new Padding(3, 5, 5, 5);
            textBoxOrderDate.Width = 150;

            // ��ס����
            Label labelCheckInDate = new Label();
            labelCheckInDate.Text = "��ס���ڣ�";
            labelCheckInDate.AutoSize = true;
            labelCheckInDate.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxCheckInDate = new TextBox();
            textBoxCheckInDate.Margin = new Padding(3, 5, 5, 5);
            textBoxCheckInDate.Width = 150;

            // �˷�����
            Label labelCheckOutDate = new Label();
            labelCheckOutDate.Text = "�˷����ڣ�";
            labelCheckOutDate.AutoSize = true;
            labelCheckOutDate.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxCheckOutDate = new TextBox();
            textBoxCheckOutDate.Margin = new Padding(3, 5, 5, 5);
            textBoxCheckOutDate.Width = 150;

            // ��������
            Label labelNumGuests = new Label();
            labelNumGuests.Text = "����������";
            labelNumGuests.AutoSize = true;
            labelNumGuests.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxNumGuests = new TextBox();
            textBoxNumGuests.Margin = new Padding(3, 5, 5, 5);
            textBoxNumGuests.Width = 150;

            // ʱ������
            Label labelTimeType = new Label();
            labelTimeType.Text = "ʱ�����ͣ�";
            labelTimeType.AutoSize = true;
            labelTimeType.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxTimeType = new TextBox();
            textBoxTimeType.Margin = new Padding(3, 5, 5, 5);
            textBoxTimeType.Width = 150;

            // �ܽ��
            Label labelTotalAmount = new Label();
            labelTotalAmount.Text = "�ܽ�";
            labelTotalAmount.AutoSize = true;
            labelTotalAmount.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxTotalAmount = new TextBox();
            textBoxTotalAmount.Margin = new Padding(3, 5, 5, 5);
            textBoxTotalAmount.Width = 150;
      

            string type = "";
            double priceday = 0;
            double pricehour = 0;
            double price = 0;

            // ����״̬
            Label labelOrderStatus = new Label();
            labelOrderStatus.Text = "����״̬��";
            labelOrderStatus.AutoSize = true;
            labelOrderStatus.Margin = new Padding(5);
            TextBox textBoxOrderStatus = new TextBox();
            textBoxOrderStatus.Margin = new Padding(5);

            // ��������
            Label labelRoomtype = new Label();
            labelRoomtype.Text = "�������ͣ�";
            labelRoomtype.AutoSize = true;
            labelRoomtype.Margin = new Padding(5);
            TextBox textBoxRoomtype = new TextBox();
            textBoxRoomtype.Margin = new Padding(5);

            // ���ı�����ӵ���񲼾����
            tableLayoutPanel.Controls.Add(labelGuestIdOrder, 0, 0);
            tableLayoutPanel.Controls.Add(textBoxGuestIdOrder, 1, 0);
            tableLayoutPanel.Controls.Add(labelCustomerContact, 0, 1);
            tableLayoutPanel.Controls.Add(textBoxCustomerContact, 1, 1);
            // ������������ؼ�����񲼾���壬�˴�ʡ�Բ����ظ����룬��ԭ���˳��һ��


            // �����������ݰ�ť
            Button buttonInsert = new Button();
            buttonInsert.Text = "�Ǽ��뷿";
            buttonInsert.AutoSize = true;
            buttonInsert.Margin = new Padding(5);

            // Ϊ�ı���ֵ
            textBoxCustomerContact.Text = "1234567890"; // �ͻ���ϵ��ʽ
            textBoxIdSh.Text = "123456789"; // ��ݱ�ʶ
            textBoxOrderId.Text = "1001"; // ����ID
            textBoxGuestIdOrder.Text = "2001"; // ����ID
            textBoxRoomId.Text = "3"; // ����ID
            textBoxOrderDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); // ��������
            textBoxCheckInDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); // ��ס����
            textBoxCheckOutDate.Text = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd"); // �˷�����
            textBoxNumGuests.Text = "2"; // ��������
            textBoxTotalAmount.Text = "500"; // �ܽ��
            textBoxTimeType.Text = "Day"; // ʱ������
            textBoxOrderStatus.Text = "��ȷ��"; // ����״̬
            textBoxRoomtype.Text = "��׼��"; // ��������



            buttonInsert.Click += (sender, e) =>
            {

                // ��ȡ����ID�ı����ֵ����ֵ��Order�������ԣ�����ת��Ϊint���ͣ�
                if (int.TryParse(textBoxRoomId.Text, out int roomIdValue))
                {
                    order.room_id = roomIdValue;
                }
                else
                {
                    order.room_id = 0;
                }

                var targetRoomStatus = roomlist.Where(room => room.RoomId == order.room_id)
                                     .Select(room => room.status)
                                     .FirstOrDefault();
                if (targetRoomStatus=="Using")
                {
                    MessageBox.Show("���� " + order.room_id + " ����ʹ��");
                }
                else
                {
                    // �ҵ���Ӧ���䲢������״̬�������ʾ����Ϊ"��Ԥ��"����ɸ���ʵ�������޸�״ֵ̬
                    var targetRoom = roomlist.Find(room => room.RoomId == order.room_id);
                    if (targetRoom != null)
                    {
                        targetRoom.status = "��Ԥ��";
                        Console.WriteLine($"���� {order.room_id} ��״̬�Ѹ���Ϊ��Ԥ��");
                    }
                    else
                    {
                        Console.WriteLine($"δ�ҵ�����IDΪ {order.room_id} �ķ�����Ϣ");
                    }
                }
                // ��ȡ���������ı����ֵ����ֵ��Order�������ԣ�����ת��Ϊint���ͣ�
                customer.id_sh = textBoxIdSh.Text;
                if (int.TryParse(textBoxNumGuests.Text, out int numGuestsValue))
                {
                    order.num_guests = numGuestsValue;
                }
                else
                {
                    order.num_guests = 0;
                }
                customer.guest_id = textBoxGuestIdOrder.Text.ToInt();
                checkIn.pay_method = textBoxTimeType.Text;
                order.order_status = textBoxOrderStatus.Text;
                order.roomtype = textBoxRoomtype.Text;
                // ��ȡ����ID�ı����ֵ����ֵ��Order�������ԣ�����ת��Ϊint���ͣ��������int.TryParse�����쳣�����
                if (int.TryParse(textBoxGuestIdOrder.Text, out int guestIdValue))
                {
                    order.guest_id = guestIdValue;
                    checkIn.guest_id = order.guest_id;
                }
                else
                {
                    // ���ת��ʧ�ܣ��ɸ���ʵ�������������ʾ��������Ĭ��ֵ0
                    order.guest_id = 0;
                }

                // ��ȡ�ܽ���ı����ֵ����ֵ��Order�������ԣ�����ת��Ϊdecimal���ͣ���decimal.TryParse�����쳣�����
                if (decimal.TryParse(textBoxTotalAmount.Text, out decimal totalAmountValue))
                {
                    order.total_amount = (double)totalAmountValue;
                }
                else
                {
                    order.total_amount = 0;
                }
                DateTime checkInDateValue;
                if (DateTime.TryParse(textBoxCheckInDate.Text, out checkInDateValue))
                {
                    order.check_in_date = checkInDateValue.ToString("yyyy-MM-dd HH:mm:ss");
                    checkIn.check_in_time = checkInDateValue;
                }
                else
                {
                    MessageBox.Show("��ס���ڸ�ʽ����ȷ���밴�� 'YYYY-MM-DD HH:mm:ss' �ĸ�ʽ����", "���ڸ�ʽ����", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                repository.QueryRoomById(order.room_id, ref type, ref priceday, ref pricehour);
                if (textBoxTimeType.Text == "Day")
                {
                    string dateString = textBoxCheckOutDate.Text;
                    DateTime checkOutDate;
                    if (DateTime.TryParse(dateString, out checkOutDate))
                    {
                        DateTime checkinDate;
                        dateString = textBoxCheckInDate.Text;
                        if (DateTime.TryParse(dateString, out checkinDate))
                        {
                            int day = DateDifference(checkinDate, checkOutDate);
                            if (day == 0)
                            {
                                day = 1;
                            }
                            price = day * priceday;
                            order.total_amount = price;
                            textBoxTotalAmount.Text = price.ToString();

                        }

                    }
                    else
                    {
                        // ת��ʧ�ܣ���ʾ�û�
                        MessageBox.Show("Invalid date format.");
                    }

                }
                else
                {
                    //todo 
                    textBoxTotalAmount.Text = "��ûд��";


                }
                DateTime checkOutDateValue;
                if (DateTime.TryParse(textBoxCheckOutDate.Text, out checkOutDateValue))
                {
                    order.check_out_date = checkOutDateValue.ToString("yyyy-MM-dd HH:mm:ss");
                    checkIn.check_out_time = checkOutDateValue;
                }
                else
                {
                    MessageBox.Show("�˷����ڸ�ʽ����ȷ���밴�� 'YYYY-MM-DD HH:mm:ss' �ĸ�ʽ����", "���ڸ�ʽ����", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                order.timetype = textBoxTimeType.Text;
                customer.customer_contact = textBoxCustomerContact.Text;


                checkIn.room_id = order.room_id;

                DateTime orderDateValue;
                if (DateTime.TryParse(textBoxOrderDate.Text, out orderDateValue))
                {
                    order.order_date = orderDateValue.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    MessageBox.Show("�������ڸ�ʽ����ȷ���밴�� 'YYYY-MM-DD HH:mm:ss' �ĸ�ʽ����", "���ڸ�ʽ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // ������ڸ�ʽ����ֱ�ӷ��أ�������ִ�к����߼�
                }

                // ��������
                orderRepository.AddOrder(order);

                // ����״̬�ı�
                repository.UpdateRoomStatus(order.room_id, "Using");

                // ������Ϣ�Ǽ�
                customerRepository.AddCustomer(customer);


                // �ǼǱ�Ĳ���
                checkInRepository.AddCheckIn(checkIn);
                roomUseTime.guest_id = customer.guest_id;
                roomUseTime.room_id = order.room_id;

                roomUseTime.check_out_time = checkOutDateValue;
                roomUseTime.check_in_time = checkInDateValue;

                // ����ʹ��ʱ������������򵥵���СʱΪ��λ���㣬ʵ�ʿ�����Ҫ�����ӵ��߼���
                if (roomUseTime.check_in_time.HasValue && roomUseTime.check_out_time.HasValue)
                {
                    roomUseTime.usage_duration = (int)(roomUseTime.check_out_time.Value - roomUseTime.check_in_time.Value).TotalHours;
                }

                roomUseTime.created_at = DateTime.UtcNow;
                roomUseTime.total_cost = price;
                roomUseTime.payment_status = "ok";

                roomUseTime.is_duration_in_hours = textBoxTimeType.Text == "Day" ? false : true;


                // ����ʹ����Ϣ

                roomUseTimeRepository.AddRoomUseTime(roomUseTime);

            };



            tableLayoutPanel.Controls.Add(labelCustomerContact, 0, 2);
            tableLayoutPanel.Controls.Add(textBoxCustomerContact, 1, 2);
            tableLayoutPanel.Controls.Add(labelIdSh, 0, 3);
            tableLayoutPanel.Controls.Add(textBoxIdSh, 1, 3);

            // ��orders��ؼ���ӵ���񲼾����
            tableLayoutPanel.Controls.Add(labelOrderId, 0, 4);
            tableLayoutPanel.Controls.Add(textBoxOrderId, 1, 4);
            tableLayoutPanel.Controls.Add(labelGuestIdOrder, 0, 5);
            tableLayoutPanel.Controls.Add(textBoxGuestIdOrder, 1, 5);
            tableLayoutPanel.Controls.Add(labelRoomId, 0, 6);
            tableLayoutPanel.Controls.Add(textBoxRoomId, 1, 6);
            tableLayoutPanel.Controls.Add(labelOrderDate, 0, 7);
            tableLayoutPanel.Controls.Add(textBoxOrderDate, 1, 7);
            tableLayoutPanel.Controls.Add(labelCheckInDate, 0, 8);
            tableLayoutPanel.Controls.Add(textBoxCheckInDate, 1, 8);
            tableLayoutPanel.Controls.Add(labelCheckOutDate, 0, 9);
            tableLayoutPanel.Controls.Add(textBoxCheckOutDate, 1, 9);
            tableLayoutPanel.Controls.Add(labelNumGuests, 0, 10);
            tableLayoutPanel.Controls.Add(textBoxNumGuests, 1, 10);
            tableLayoutPanel.Controls.Add(labelTotalAmount, 0, 11);
            tableLayoutPanel.Controls.Add(textBoxTotalAmount, 1, 11);
            tableLayoutPanel.Controls.Add(labelTimeType, 0, 12);
            tableLayoutPanel.Controls.Add(textBoxTimeType, 1, 12);
            tableLayoutPanel.Controls.Add(labelOrderStatus, 0, 13);
            tableLayoutPanel.Controls.Add(textBoxOrderStatus, 1, 13);

            // ��ӷ������Ϳؼ�����񲼾����
            tableLayoutPanel.Controls.Add(labelRoomtype, 0, 14);
            tableLayoutPanel.Controls.Add(textBoxRoomtype, 1, 14);

            tableLayoutPanel.Controls.Add(buttonInsert, 1, 15);

            // ����񲼾������ӵ�splitContainer1.Panel2
            splitContainer1.Panel2.Controls.Add(tableLayoutPanel);

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void DisplayRoomServices()
        {
            List<RoomService> roomlist;
            try
            {
                roomlist = repository.GetAllRoomServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ȡ�����������ʱ���ִ���: {ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;

            foreach (RoomService service in roomlist)
            {
                Panel panel = new Panel();
                panel.BackColor = Color.Black;
                panel.Size = new Size(200, 80);

                Label labelId = new Label();
                labelId.ForeColor = Color.White;
                labelId.Text = $"������: {service.RoomId}";
                labelId.Location = new Point(10, 10);
                labelId.AutoSize = true;
                panel.Controls.Add(labelId);

                Label labelStatus = new Label();
                labelStatus.ForeColor = Color.White;
                labelStatus.Text = service.status == "Available" ? "״̬: ����" : "״̬: ʹ����";
                panel.BackColor = service.status == "Available" ? Color.Green : Color.Red;
                labelStatus.Location = new Point(10, 30);
                labelStatus.AutoSize = true;
                panel.Controls.Add(labelStatus);

                flowLayoutPanel1.Controls.Add(panel);
            }

            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(flowLayoutPanel1);
        }
        private void uiButton5_Click(object sender, EventArgs e)
        {
            DisplayRoomServices();
        }
        IRoomUseTimeRepository roomUseTimeRepository = new RoomUseTimeRepository();
        private void uiButton2_Click(object sender, EventArgs e)
        {
           
            // ���splitContainer1.Panel2.Controls�����е����пؼ�
            splitContainer1.Panel2.Controls.Clear();

            // ����TableLayoutPanel���ڲ���
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

            // �����������뷿��ID�ı�ǩ���ı���
            Label labelRoomId = new Label();
            labelRoomId.Text = "����ID��";
            labelRoomId.AutoSize = true;
            labelRoomId.Margin = new Padding(5);
            TextBox textBoxRoomId = new TextBox();
            textBoxRoomId.Margin = new Padding(5);
            textBoxRoomId.Text = "1";
            // ����������ʾ�������ֵı�ǩ����Ӧ��������ʾ��ǩ
            Label labelGuestName = new Label();
            labelGuestName.Text = "�������֣�";
            labelGuestName.AutoSize = true;
            labelGuestName.Margin = new Padding(5);
            Label labelGuestNameContent = new Label();
            labelGuestNameContent.AutoSize = true;
            labelGuestNameContent.Margin = new Padding(5);
            
            // ����������ʾ���͵绰�ı�ǩ����Ӧ��������ʾ��ǩ
            Label labelGuestPhone = new Label();
            labelGuestPhone.Text = "���͵绰��";
            labelGuestPhone.AutoSize = true;
            labelGuestPhone.Margin = new Padding(5);
            Label labelGuestPhoneContent = new Label();
            labelGuestPhoneContent.AutoSize = true;
            labelGuestPhoneContent.Margin = new Padding(5);

            // ����������ʾ�������͵ı�ǩ����Ӧ��������ʾ��ǩ
            Label labelRoomInfo1 = new Label();
            labelRoomInfo1.Text = "�������ͣ�";
            labelRoomInfo1.AutoSize = true;
            labelRoomInfo1.Margin = new Padding(5);
            Label labelRoomInfo1Content = new Label();
            labelRoomInfo1Content.AutoSize = true;
            labelRoomInfo1Content.Margin = new Padding(5);

            // ����������ʾ����״̬�ı�ǩ����Ӧ��������ʾ��ǩ
            Label labelRoomInfo2 = new Label();
            labelRoomInfo2.Text = "����״̬��";
            labelRoomInfo2.AutoSize = true;
            labelRoomInfo2.Margin = new Padding(5);
            Label labelRoomInfo2Content = new Label();
            labelRoomInfo2Content.AutoSize = true;
            labelRoomInfo2Content.Margin = new Padding(5);
           
            // ������ť
            Button buttonGetInfo = new Button();
            buttonGetInfo.Text = "��ȡ������Ϣ";
            buttonGetInfo.AutoSize = true;
            buttonGetInfo.Margin = new Padding(5);

            // ���ؼ���ӵ�TableLayoutPanel��
            tableLayoutPanel.Controls.Add(labelRoomId, 0, 0);
            tableLayoutPanel.Controls.Add(textBoxRoomId, 1, 0);
            tableLayoutPanel.Controls.Add(labelGuestName, 0, 1);
            tableLayoutPanel.Controls.Add(labelGuestNameContent, 1, 1);
            tableLayoutPanel.Controls.Add(labelGuestPhone, 0, 2);
            tableLayoutPanel.Controls.Add(labelGuestPhoneContent, 1, 2);
            tableLayoutPanel.Controls.Add(labelRoomInfo1, 0, 3);
            tableLayoutPanel.Controls.Add(labelRoomInfo1Content, 1, 3);
            tableLayoutPanel.Controls.Add(labelRoomInfo2, 0, 4);
            tableLayoutPanel.Controls.Add(labelRoomInfo2Content, 1, 4);
            tableLayoutPanel.Controls.Add(buttonGetInfo, 1, 5);

            buttonGetInfo.Click += (senderObj, eArgs) =>
            {
                // ģ���ĳ���ط���ȡ������ݣ������ü򵥵Ĺ̶�ֵʾ����ʵ�����滻��ʵ�߼�
                RoomUseTime roomUseTime = new RoomUseTime();
                roomUseTime.room_id = textBoxRoomId.Text.ToInt();

                roomUseTime=roomUseTimeRepository.GetRoomUseTimeById(roomUseTime.room_id);

                labelGuestNameContent.Text = roomUseTime.guest_id.ToString();
                //labelGuestPhoneContent.Text = roomUseTime.();
                //labelRoomInfo1Content.Text = roomType;
                //labelRoomInfo2Content.Text = roomUseTime;
            };
            // ��TableLayoutPanel��ӵ�splitContainer1.Panel2.Controls������
            splitContainer1.Panel2.Controls.Add(tableLayoutPanel);

        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            // ������ж���
            List<Order> orders= orderRepository.GetAllOrders();
            // ����DataTable���洢��������
            DataTable dataTable = new DataTable();

            // �����
            dataTable.Columns.Add("����ID", typeof(int));
            dataTable.Columns.Add("�ͻ�ID", typeof(int));
            dataTable.Columns.Add("����ID", typeof(int));
            dataTable.Columns.Add("��������", typeof(string));
            dataTable.Columns.Add("��ס����", typeof(string));
            dataTable.Columns.Add("�˷�����", typeof(string));
            dataTable.Columns.Add("��������", typeof(int));
            dataTable.Columns.Add("�ܽ��", typeof(double));
            dataTable.Columns.Add("ʱ������", typeof(string));
            dataTable.Columns.Add("����״̬", typeof(string));
            dataTable.Columns.Add("��������", typeof(string));

            // �������
            foreach (Order order in orders)
            {
                splitContainer1.Panel2.Controls.Clear();
                DataRow row = dataTable.NewRow();
                row["����ID"] = order.order_id;
                row["�ͻ�ID"] = order.guest_id;
                row["����ID"] = order.room_id;
                row["��������"] = order.order_date;
                row["��ס����"] = order.check_in_date;
                row["�˷�����"] = order.check_out_date;
                row["��������"] = order.num_guests;
                row["�ܽ��"] = order.total_amount;
                row["ʱ������"] = order.timetype;
                row["����״̬"] = order.order_status;
                row["��������"] = order.roomtype;
                dataTable.Rows.Add(row);
            }
           
            // ��DataTable�󶨵�DataGridView
            DataGridView dataGridView1 = new DataGridView();
            // ����dataGridView1���������ʹ����������Сһ��
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.DataSource = dataTable;
            dataGridView1.BackgroundColor = Color.White;
            splitContainer1.Panel2.Controls.Add(dataGridView1);
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            // ��ȡ����
            List<RoomUseTime> roomUseTimes = roomUseTimeRepository.GetRoomUseTimeRecordsBeforeNow();

            // ����DataGridView����չʾ����
            DataGridView dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ����DataTable���洢���ݣ��Ա�󶨵�DataGridView
            DataTable dataTable = new DataTable();

            // ����У�����RoomUseTime��������������У������"�Ƿ������"��
            dataTable.Columns.Add("����ID", typeof(int));
            dataTable.Columns.Add("��סʱ��", typeof(DateTime));
            dataTable.Columns.Add("�˷�ʱ��", typeof(DateTime));
            dataTable.Columns.Add("�ܷ���", typeof(decimal));
            dataTable.Columns.Add("�Ƿ������", typeof(bool));
           
            // �ɸ���RoomUseTime����������Լ�����Ӹ�����

            // ������ݵ�DataTable
            foreach (RoomUseTime roomUseTime in roomUseTimes)
            {
                DataRow row = dataTable.NewRow();
                row["����ID"] = roomUseTime.room_id;
                row["��סʱ��"] = roomUseTime.check_in_time;
                row["�˷�ʱ��"] = roomUseTime.check_out_time;
                row["�ܷ���"] = roomUseTime.total_cost;
                // ������������߼����ж��Ƿ�����ɣ�ʾ���м���Ϊfalse�������滻Ϊ��ʵ�߼�
                row["�Ƿ������"] = true;
                orderRepository.UpdateOrderStatus(roomUseTime.room_id.ToString().ToInt());
                dataTable.Rows.Add(row);
                repository.UpdateRoomStatus(roomUseTime.room_id.ToString().ToInt(), "Available");


            }
            if (dataTable.Columns.Contains("�Ƿ������"))
            {
                dataTable.Columns["�Ƿ������"].ReadOnly = true;
            }
            else
            {
                Console.WriteLine("ָ�����в�������DataTable��");
            }
            // ��DataTable�󶨵�DataGridView
            dataGridView.DataSource = dataTable;
            dataGridView.BackgroundColor = Color.White;
            // ��DataGridView��ӵ�splitContainer1.Panel2������
            splitContainer1.Panel2.Controls.Add(dataGridView);
        }
    }
}
