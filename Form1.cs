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
            // 获得数据
            
           
            try
            {
                roomlist = repository.GetAllRoomServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取房间服务数据时出现错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // 清除splitContainer1.Panel2中的现有控件
            splitContainer1.Panel2.Controls.Clear();

            // 创建一个表格布局面板
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.RowCount = 18; // 增加一行
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

            // 客人ID
            Label labelGuestIdOrder = new Label();
            labelGuestIdOrder.Text = "客人ID：";
            labelGuestIdOrder.AutoSize = true;
            labelGuestIdOrder.Margin = new Padding(3, 5, 0, 5); // 减少左侧Margin值，使文本框更靠近标签
            TextBox textBoxGuestIdOrder = new TextBox();
            textBoxGuestIdOrder.Margin = new Padding(3, 5, 5, 5);
            textBoxGuestIdOrder.Width = 150; // 设置文本框宽度，使其更长

            // 客户联系方式
            Label labelCustomerContact = new Label();
            labelCustomerContact.Text = "客户联系方式：";
            labelCustomerContact.AutoSize = true;
            labelCustomerContact.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxCustomerContact = new TextBox();
            textBoxCustomerContact.Margin = new Padding(3, 5, 5, 5);
            textBoxCustomerContact.Width = 150;

            // 身份标识
            Label labelIdSh = new Label();
            labelIdSh.Text = "身份标识：";
            labelIdSh.AutoSize = true;
            labelIdSh.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxIdSh = new TextBox();
            textBoxIdSh.Margin = new Padding(3, 5, 5, 5);
            textBoxIdSh.Width = 150;

            // 订单ID
            Label labelOrderId = new Label();
            labelOrderId.Text = "订单ID：";
            labelOrderId.AutoSize = true;
            labelOrderId.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxOrderId = new TextBox();
            textBoxOrderId.Margin = new Padding(3, 5, 5, 5);
            textBoxOrderId.Width = 150;

            // 房间ID
            Label labelRoomId = new Label();
            labelRoomId.Text = "房间ID：";
            labelRoomId.AutoSize = true;
            labelRoomId.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxRoomId = new TextBox();
            textBoxRoomId.Margin = new Padding(3, 5, 5, 5);
            textBoxRoomId.Width = 150;

            // 订单日期
            Label labelOrderDate = new Label();
            labelOrderDate.Text = "订单日期：";
            labelOrderDate.AutoSize = true;
            labelOrderDate.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxOrderDate = new TextBox();
            textBoxOrderDate.Margin = new Padding(3, 5, 5, 5);
            textBoxOrderDate.Width = 150;

            // 入住日期
            Label labelCheckInDate = new Label();
            labelCheckInDate.Text = "入住日期：";
            labelCheckInDate.AutoSize = true;
            labelCheckInDate.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxCheckInDate = new TextBox();
            textBoxCheckInDate.Margin = new Padding(3, 5, 5, 5);
            textBoxCheckInDate.Width = 150;

            // 退房日期
            Label labelCheckOutDate = new Label();
            labelCheckOutDate.Text = "退房日期：";
            labelCheckOutDate.AutoSize = true;
            labelCheckOutDate.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxCheckOutDate = new TextBox();
            textBoxCheckOutDate.Margin = new Padding(3, 5, 5, 5);
            textBoxCheckOutDate.Width = 150;

            // 客人数量
            Label labelNumGuests = new Label();
            labelNumGuests.Text = "客人数量：";
            labelNumGuests.AutoSize = true;
            labelNumGuests.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxNumGuests = new TextBox();
            textBoxNumGuests.Margin = new Padding(3, 5, 5, 5);
            textBoxNumGuests.Width = 150;

            // 时间类型
            Label labelTimeType = new Label();
            labelTimeType.Text = "时间类型：";
            labelTimeType.AutoSize = true;
            labelTimeType.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxTimeType = new TextBox();
            textBoxTimeType.Margin = new Padding(3, 5, 5, 5);
            textBoxTimeType.Width = 150;

            // 总金额
            Label labelTotalAmount = new Label();
            labelTotalAmount.Text = "总金额：";
            labelTotalAmount.AutoSize = true;
            labelTotalAmount.Margin = new Padding(3, 5, 0, 5);
            TextBox textBoxTotalAmount = new TextBox();
            textBoxTotalAmount.Margin = new Padding(3, 5, 5, 5);
            textBoxTotalAmount.Width = 150;
      

            string type = "";
            double priceday = 0;
            double pricehour = 0;
            double price = 0;

            // 订单状态
            Label labelOrderStatus = new Label();
            labelOrderStatus.Text = "订单状态：";
            labelOrderStatus.AutoSize = true;
            labelOrderStatus.Margin = new Padding(5);
            TextBox textBoxOrderStatus = new TextBox();
            textBoxOrderStatus.Margin = new Padding(5);

            // 房间类型
            Label labelRoomtype = new Label();
            labelRoomtype.Text = "房间类型：";
            labelRoomtype.AutoSize = true;
            labelRoomtype.Margin = new Padding(5);
            TextBox textBoxRoomtype = new TextBox();
            textBoxRoomtype.Margin = new Padding(5);

            // 将文本框添加到表格布局面板
            tableLayoutPanel.Controls.Add(labelGuestIdOrder, 0, 0);
            tableLayoutPanel.Controls.Add(textBoxGuestIdOrder, 1, 0);
            tableLayoutPanel.Controls.Add(labelCustomerContact, 0, 1);
            tableLayoutPanel.Controls.Add(textBoxCustomerContact, 1, 1);
            // 依次添加其他控件到表格布局面板，此处省略部分重复代码，和原添加顺序一致


            // 创建插入数据按钮
            Button buttonInsert = new Button();
            buttonInsert.Text = "登记入房";
            buttonInsert.AutoSize = true;
            buttonInsert.Margin = new Padding(5);

            // 为文本框赋值
            textBoxCustomerContact.Text = "1234567890"; // 客户联系方式
            textBoxIdSh.Text = "123456789"; // 身份标识
            textBoxOrderId.Text = "1001"; // 订单ID
            textBoxGuestIdOrder.Text = "2001"; // 客人ID
            textBoxRoomId.Text = "3"; // 房间ID
            textBoxOrderDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); // 订单日期
            textBoxCheckInDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); // 入住日期
            textBoxCheckOutDate.Text = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd"); // 退房日期
            textBoxNumGuests.Text = "2"; // 客人数量
            textBoxTotalAmount.Text = "500"; // 总金额
            textBoxTimeType.Text = "Day"; // 时间类型
            textBoxOrderStatus.Text = "已确认"; // 订单状态
            textBoxRoomtype.Text = "标准间"; // 房间类型



            buttonInsert.Click += (sender, e) =>
            {

                // 获取房间ID文本框的值并赋值给Order对象属性（尝试转换为int类型）
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
                    MessageBox.Show("房间 " + order.room_id + " 正在使用");
                }
                else
                {
                    // 找到对应房间并更改其状态，这里简单示例改为"已预订"，你可根据实际需求修改状态值
                    var targetRoom = roomlist.Find(room => room.RoomId == order.room_id);
                    if (targetRoom != null)
                    {
                        targetRoom.status = "已预订";
                        Console.WriteLine($"房间 {order.room_id} 的状态已更改为已预订");
                    }
                    else
                    {
                        Console.WriteLine($"未找到房间ID为 {order.room_id} 的房间信息");
                    }
                }
                // 获取客人数量文本框的值并赋值给Order对象属性（尝试转换为int类型）
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
                // 获取客人ID文本框的值并赋值给Order对象属性（尝试转换为int类型，这里简单用int.TryParse处理异常情况）
                if (int.TryParse(textBoxGuestIdOrder.Text, out int guestIdValue))
                {
                    order.guest_id = guestIdValue;
                    checkIn.guest_id = order.guest_id;
                }
                else
                {
                    // 如果转换失败，可根据实际情况处理，这里示例还是用默认值0
                    order.guest_id = 0;
                }

                // 获取总金额文本框的值并赋值给Order对象属性（尝试转换为decimal类型，用decimal.TryParse处理异常情况）
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
                    MessageBox.Show("入住日期格式不正确，请按照 'YYYY-MM-DD HH:mm:ss' 的格式输入", "日期格式错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                        // 转换失败，提示用户
                        MessageBox.Show("Invalid date format.");
                    }

                }
                else
                {
                    //todo 
                    textBoxTotalAmount.Text = "还没写完";


                }
                DateTime checkOutDateValue;
                if (DateTime.TryParse(textBoxCheckOutDate.Text, out checkOutDateValue))
                {
                    order.check_out_date = checkOutDateValue.ToString("yyyy-MM-dd HH:mm:ss");
                    checkIn.check_out_time = checkOutDateValue;
                }
                else
                {
                    MessageBox.Show("退房日期格式不正确，请按照 'YYYY-MM-DD HH:mm:ss' 的格式输入", "日期格式错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    MessageBox.Show("订单日期格式不正确，请按照 'YYYY-MM-DD HH:mm:ss' 的格式输入", "日期格式错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // 如果日期格式错误，直接返回，不继续执行后续逻辑
                }

                // 订单插入
                orderRepository.AddOrder(order);

                // 房间状态改变
                repository.UpdateRoomStatus(order.room_id, "Using");

                // 房客信息登记
                customerRepository.AddCustomer(customer);


                // 登记表的插入
                checkInRepository.AddCheckIn(checkIn);
                roomUseTime.guest_id = customer.guest_id;
                roomUseTime.room_id = order.room_id;

                roomUseTime.check_out_time = checkOutDateValue;
                roomUseTime.check_in_time = checkInDateValue;

                // 计算使用时长（假设这里简单地以小时为单位计算，实际可能需要更复杂的逻辑）
                if (roomUseTime.check_in_time.HasValue && roomUseTime.check_out_time.HasValue)
                {
                    roomUseTime.usage_duration = (int)(roomUseTime.check_out_time.Value - roomUseTime.check_in_time.Value).TotalHours;
                }

                roomUseTime.created_at = DateTime.UtcNow;
                roomUseTime.total_cost = price;
                roomUseTime.payment_status = "ok";

                roomUseTime.is_duration_in_hours = textBoxTimeType.Text == "Day" ? false : true;


                // 房间使用信息

                roomUseTimeRepository.AddRoomUseTime(roomUseTime);

            };



            tableLayoutPanel.Controls.Add(labelCustomerContact, 0, 2);
            tableLayoutPanel.Controls.Add(textBoxCustomerContact, 1, 2);
            tableLayoutPanel.Controls.Add(labelIdSh, 0, 3);
            tableLayoutPanel.Controls.Add(textBoxIdSh, 1, 3);

            // 将orders表控件添加到表格布局面板
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

            // 添加房间类型控件到表格布局面板
            tableLayoutPanel.Controls.Add(labelRoomtype, 0, 14);
            tableLayoutPanel.Controls.Add(textBoxRoomtype, 1, 14);

            tableLayoutPanel.Controls.Add(buttonInsert, 1, 15);

            // 将表格布局面板添加到splitContainer1.Panel2
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
                MessageBox.Show($"获取房间服务数据时出现错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                labelId.Text = $"房间编号: {service.RoomId}";
                labelId.Location = new Point(10, 10);
                labelId.AutoSize = true;
                panel.Controls.Add(labelId);

                Label labelStatus = new Label();
                labelStatus.ForeColor = Color.White;
                labelStatus.Text = service.status == "Available" ? "状态: 可用" : "状态: 使用中";
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
           
            // 清空splitContainer1.Panel2.Controls容器中的现有控件
            splitContainer1.Panel2.Controls.Clear();

            // 创建TableLayoutPanel用于布局
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

            // 创建用于输入房间ID的标签和文本框
            Label labelRoomId = new Label();
            labelRoomId.Text = "房间ID：";
            labelRoomId.AutoSize = true;
            labelRoomId.Margin = new Padding(5);
            TextBox textBoxRoomId = new TextBox();
            textBoxRoomId.Margin = new Padding(5);
            textBoxRoomId.Text = "1";
            // 创建用于显示房客名字的标签及对应的内容显示标签
            Label labelGuestName = new Label();
            labelGuestName.Text = "房客名字：";
            labelGuestName.AutoSize = true;
            labelGuestName.Margin = new Padding(5);
            Label labelGuestNameContent = new Label();
            labelGuestNameContent.AutoSize = true;
            labelGuestNameContent.Margin = new Padding(5);
            
            // 创建用于显示房客电话的标签及对应的内容显示标签
            Label labelGuestPhone = new Label();
            labelGuestPhone.Text = "房客电话：";
            labelGuestPhone.AutoSize = true;
            labelGuestPhone.Margin = new Padding(5);
            Label labelGuestPhoneContent = new Label();
            labelGuestPhoneContent.AutoSize = true;
            labelGuestPhoneContent.Margin = new Padding(5);

            // 创建用于显示房间类型的标签及对应的内容显示标签
            Label labelRoomInfo1 = new Label();
            labelRoomInfo1.Text = "房间类型：";
            labelRoomInfo1.AutoSize = true;
            labelRoomInfo1.Margin = new Padding(5);
            Label labelRoomInfo1Content = new Label();
            labelRoomInfo1Content.AutoSize = true;
            labelRoomInfo1Content.Margin = new Padding(5);

            // 创建用于显示房间状态的标签及对应的内容显示标签
            Label labelRoomInfo2 = new Label();
            labelRoomInfo2.Text = "房间状态：";
            labelRoomInfo2.AutoSize = true;
            labelRoomInfo2.Margin = new Padding(5);
            Label labelRoomInfo2Content = new Label();
            labelRoomInfo2Content.AutoSize = true;
            labelRoomInfo2Content.Margin = new Padding(5);
           
            // 创建按钮
            Button buttonGetInfo = new Button();
            buttonGetInfo.Text = "获取房间信息";
            buttonGetInfo.AutoSize = true;
            buttonGetInfo.Margin = new Padding(5);

            // 将控件添加到TableLayoutPanel中
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
                // 模拟从某个地方获取相关数据，这里用简单的固定值示例，实际需替换真实逻辑
                RoomUseTime roomUseTime = new RoomUseTime();
                roomUseTime.room_id = textBoxRoomId.Text.ToInt();

                roomUseTime=roomUseTimeRepository.GetRoomUseTimeById(roomUseTime.room_id);

                labelGuestNameContent.Text = roomUseTime.guest_id.ToString();
                //labelGuestPhoneContent.Text = roomUseTime.();
                //labelRoomInfo1Content.Text = roomType;
                //labelRoomInfo2Content.Text = roomUseTime;
            };
            // 将TableLayoutPanel添加到splitContainer1.Panel2.Controls容器中
            splitContainer1.Panel2.Controls.Add(tableLayoutPanel);

        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            // 获得所有订单
            List<Order> orders= orderRepository.GetAllOrders();
            // 创建DataTable来存储订单数据
            DataTable dataTable = new DataTable();

            // 添加列
            dataTable.Columns.Add("订单ID", typeof(int));
            dataTable.Columns.Add("客户ID", typeof(int));
            dataTable.Columns.Add("房间ID", typeof(int));
            dataTable.Columns.Add("订单日期", typeof(string));
            dataTable.Columns.Add("入住日期", typeof(string));
            dataTable.Columns.Add("退房日期", typeof(string));
            dataTable.Columns.Add("客人数量", typeof(int));
            dataTable.Columns.Add("总金额", typeof(double));
            dataTable.Columns.Add("时间类型", typeof(string));
            dataTable.Columns.Add("订单状态", typeof(string));
            dataTable.Columns.Add("房间类型", typeof(string));

            // 填充数据
            foreach (Order order in orders)
            {
                splitContainer1.Panel2.Controls.Clear();
                DataRow row = dataTable.NewRow();
                row["订单ID"] = order.order_id;
                row["客户ID"] = order.guest_id;
                row["房间ID"] = order.room_id;
                row["订单日期"] = order.order_date;
                row["入住日期"] = order.check_in_date;
                row["退房日期"] = order.check_out_date;
                row["客人数量"] = order.num_guests;
                row["总金额"] = order.total_amount;
                row["时间类型"] = order.timetype;
                row["订单状态"] = order.order_status;
                row["房间类型"] = order.roomtype;
                dataTable.Rows.Add(row);
            }
           
            // 将DataTable绑定到DataGridView
            DataGridView dataGridView1 = new DataGridView();
            // 设置dataGridView1的相关属性使其与容器大小一致
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.DataSource = dataTable;
            dataGridView1.BackgroundColor = Color.White;
            splitContainer1.Panel2.Controls.Add(dataGridView1);
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            // 获取数据
            List<RoomUseTime> roomUseTimes = roomUseTimeRepository.GetRoomUseTimeRecordsBeforeNow();

            // 创建DataGridView用于展示数据
            DataGridView dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 创建DataTable来存储数据，以便绑定到DataGridView
            DataTable dataTable = new DataTable();

            // 添加列，根据RoomUseTime类的属性来定义列，并添加"是否已完成"列
            dataTable.Columns.Add("房间ID", typeof(int));
            dataTable.Columns.Add("入住时间", typeof(DateTime));
            dataTable.Columns.Add("退房时间", typeof(DateTime));
            dataTable.Columns.Add("总费用", typeof(decimal));
            dataTable.Columns.Add("是否已完成", typeof(bool));
           
            // 可根据RoomUseTime类的其他属性继续添加更多列

            // 填充数据到DataTable
            foreach (RoomUseTime roomUseTime in roomUseTimes)
            {
                DataRow row = dataTable.NewRow();
                row["房间ID"] = roomUseTime.room_id;
                row["入住时间"] = roomUseTime.check_in_time;
                row["退房时间"] = roomUseTime.check_out_time;
                row["总费用"] = roomUseTime.total_cost;
                // 这里假设你有逻辑来判断是否已完成，示例中简单设为false，你需替换为真实逻辑
                row["是否已完成"] = true;
                orderRepository.UpdateOrderStatus(roomUseTime.room_id.ToString().ToInt());
                dataTable.Rows.Add(row);
                repository.UpdateRoomStatus(roomUseTime.room_id.ToString().ToInt(), "Available");


            }
            if (dataTable.Columns.Contains("是否已完成"))
            {
                dataTable.Columns["是否已完成"].ReadOnly = true;
            }
            else
            {
                Console.WriteLine("指定的列不存在于DataTable中");
            }
            // 将DataTable绑定到DataGridView
            dataGridView.DataSource = dataTable;
            dataGridView.BackgroundColor = Color.White;
            // 将DataGridView添加到splitContainer1.Panel2容器中
            splitContainer1.Panel2.Controls.Add(dataGridView);
        }
    }
}
