# TempMonitor - 温湿度监控系统

一个功能完善的基于 .NET Framework 4.8 的 Windows Forms 应用程序，用于通过 Modbus 协议实时监控温湿度数据，支持数据记录、报警、导出等功能。

## 技术栈

| 组件        | 库/框架            | 版本  | 用途             |
| ----------- | ------------------ | ----- | ---------------- |
| 运行时      | .NET Framework     | 4.8   | 基础运行环境     |
| UI 框架     | Windows Forms      | -     | 桌面应用界面     |
| 数据库      | System.Data.SQLite | 2.0.3 | 轻量级本地数据库 |
| Modbus 通信 | NModbus4           | 2.1.0 | 工业设备通信协议 |
| Excel 处理  | NPOI               | 2.8.0 | 数据导出为 Excel |
| 日志记录    | NLog               | 6.1.3 | 系统日志管理     |

## 项目结构

```
TempMonitor/
├── TempMonitor/              # 主项目目录
│   ├── Config/               # 配置相关
│   │   └── AppConfig.cs      # 应用配置类
│   ├── Data/                 # 数据库层
│   │   └── DatabaseService.cs # SQLite 数据库服务
│   ├── Models/               # 数据模型
│   │   ├── AlarmRecord.cs    # 报警记录模型
│   │   ├── OperationLog.cs   # 操作日志模型
│   │   ├── SensorData.cs     # 传感器数据模型
│   │   └── UserInfo.cs       # 用户信息模型
│   ├── Presentation/         # UI 层
│   │   ├── LoginForm.cs      # 登录窗体
│   │   ├── MainForm.cs       # 主窗体
│   │   ├── HistoryForm.cs    # 历史数据查询窗体
│   │   └── SettingsForm.cs   # 设置窗体
│   ├── Service/              # 业务服务层
│   │   ├── Interfaces/       # 服务接口定义
│   │   │   ├── IAlarmService.cs
│   │   │   ├── IExportService.cs
│   │   │   ├── IModbusService.cs
│   │   │   ├── IOperationLogService.cs
│   │   │   └── IUserService.cs
│   │   ├── AlarmService.cs   # 报警服务
│   │   ├── ExportService.cs  # 数据导出服务
│   │   ├── ModbusService.cs  # Modbus 通信服务
│   │   ├── OperationLogService.cs # 操作日志服务
│   │   └── UserService.cs    # 用户服务
│   ├── Program.cs            # 程序入口点
│   ├── appsettings.json      # 应用配置文件
│   └── TempMonitor.csproj    # 项目文件
├── packages/                 # NuGet 包目录
└── TempMonitor.slnx          # 解决方案文件
```

### 架构设计

项目采用简单的分层架构：

1. **Presentation 层**：负责 UI 展示和用户交互
2. **Service 层**：封装业务逻辑，通过接口定义契约
3. **Data 层**：负责数据持久化和数据库操作
4. **Models 层**：定义数据结构和模型
5. **Config 层**：管理应用配置

## 功能特性

### 核心功能

- **实时数据采集** - 通过串口 Modbus 协议实时采集温湿度数据，可配置采集间隔
- **可视化图表** - 使用 Chart 控件实时展示温湿度变化曲线
- **状态监控** - 实时显示连接状态、运行时间和数据采集数量
- **智能报警** - 支持设置温湿度上下限阈值，超限自动弹窗报警并记录

### 数据管理

- **数据持久化** - 自动将采集的数据保存到 SQLite 数据库
- **历史查询** - 全新的历史数据查询界面，支持按时间范围查询
- **数据浏览** - 表格形式展示历史数据，方便查看
- **数据导出** - 支持导出为 Excel (.xlsx) 和 CSV 格式
- **自动清理** - 可配置数据保留天数，自动清理过期数据

### 系统管理

- **用户认证** - 支持登录认证，保护系统安全
- **角色权限** - 管理员和操作员双重角色控制
- **操作日志** - 完整记录用户的所有操作行为
- **异常处理** - 全局异常捕获，记录错误日志

## 界面预览

- **主界面** - 实时显示温湿度数值、曲线图表和系统日志，包含「历史数据」按钮
- **登录界面** - 用户身份验证入口
- **历史查询界面** - 按时间范围查询历史数据，支持导出为 CSV
- **设置界面** - 配置报警阈值、串口参数等

### 配置项详解

| 配置项              | 类型   | 默认值   | 说明                                       |
| ------------------- | ------ | -------- | ------------------------------------------ |
| `PortName`          | string | `"COM3"` | 串口名称，需根据实际设备连接修改           |
| `BaudRate`          | int    | `9600`   | 串口波特率                                 |
| `Interval`          | int    | `1000`   | 数据采集间隔，单位：毫秒                   |
| `TempAlarmHigh`     | double | `30.0`   | 温度高限报警阈值，单位：℃                  |
| `TempAlarmLow`      | double | `5.0`    | 温度低限报警阈值，单位：℃                  |
| `HumidAlarmHigh`    | double | `90.0`   | 湿度高限报警阈值，单位：%                  |
| `HumidAlarmLow`     | double | `20.0`   | 湿度低限报警阈值，单位：%                  |
| `DataRetentionDays` | int    | `90`     | 数据保留天数，超过此天数的数据会被自动清理 |

## 快速开始

### 环境要求

- **操作系统**：Windows 7 及以上版本
- **.NET Framework**：4.8 或更高版本
- **硬件**：支持串口的计算机，需连接 Modbus 温湿度传感器
- **开发工具**（如需编译）：Visual Studio 2022/2026

### 安装运行

#### 方式一：直接运行编译好的程序

1. 下载 `bin/Debug/` 目录下的所有文件
2. 双击 `TempMonitor.exe` 运行
3. 首次使用默认账号登录

#### 方式二：从源码编译

1. 克隆或下载项目代码到本地
2. 使用 Visual Studio 2022 打开 `TempMonitor.slnx`
3. 在解决方案资源管理器中右键点击解决方案，选择「还原 NuGet 程序包」
4. 选择「x64」平台和「Debug」或「Release」配置
5. 按 `F5` 或点击「开始」按钮编译并运行

### 默认登录账号

| 用户名  | 密码    | 角色   | 权限         |
| ------- | ------- | ------ | ------------ |
| `admin` | `admin` | 管理员 | 完整系统权限 |

> ⚠️ 安全提示：首次登录后请立即修改默认密码！

## 配置说明

配置文件 `appsettings.json` 位于程序根目录，程序启动时会自动加载：

```json
{
  "PortName": "COM3",
  "BaudRate": 9600,
  "Interval": 1000,
  "TempAlarmHigh": 30.0,
  "TempAlarmLow": 5.0,
  "HumidAlarmHigh": 90.0,
  "HumidAlarmLow": 20.0,
  "DataRetentionDays": 90
}
```

## 数据库说明

系统使用 SQLite 作为本地数据库，数据库文件位于 `Data/TempMonitor.db`。

### 数据表结构

#### SensorData（传感器数据表）

| 字段        | 类型    | 说明        |
| ----------- | ------- | ----------- |
| Id          | INTEGER | 主键，自增  |
| Temperature | REAL    | 温度值（℃） |
| Humidity    | REAL    | 湿度值（%） |
| RecordTime  | TEXT    | 记录时间    |

#### Users（用户表）

| 字段     | 类型    | 说明                   |
| -------- | ------- | ---------------------- |
| Id       | INTEGER | 主键，自增             |
| Username | TEXT    | 用户名（唯一）         |
| Password | TEXT    | 密码                   |
| Role     | TEXT    | 角色（Admin/Operator） |

#### AlarmRecords（报警记录表）

| 字段       | 类型    | 说明       |
| ---------- | ------- | ---------- |
| Id         | INTEGER | 主键，自增 |
| AlarmType  | TEXT    | 报警类型   |
| AlarmValue | REAL    | 报警数值   |
| Level      | TEXT    | 报警级别   |
| RecordTime | TEXT    | 记录时间   |
| Confirmed  | INTEGER | 是否已确认 |

#### OperationLogs（操作日志表）

| 字段       | 类型    | 说明       |
| ---------- | ------- | ---------- |
| Id         | INTEGER | 主键，自增 |
| Username   | TEXT    | 操作用户名 |
| Action     | TEXT    | 操作内容   |
| RecordTime | TEXT    | 记录时间   |

## 使用说明

### 基本操作流程

1. **启动程序** - 运行 TempMonitor.exe
2. **登录系统** - 输入用户名和密码登录
3. **配置参数** - 在设置界面配置串口和报警阈值
4. **开始采集** - 点击「启动」按钮开始数据采集
5. **查看数据** - 在主界面实时查看温湿度数据和曲线
6. **查询历史** - 点击「历史数据」按钮查询和导出历史记录
7. **导出数据** - 在历史查询界面或主界面导出历史数据
8. **停止采集** - 点击「停止」按钮结束数据采集

### 历史数据查询

历史数据查询功能使用方法：

1. 在主界面点击「历史数据」按钮
2. 选择查询的开始和结束时间
3. 点击「查询」按钮
4. 数据会在表格中显示
5. 点击「导出CSV」可将查询结果导出为 CSV 格式

### 报警处理

当温湿度超出设定的阈值范围时：

- 系统会自动弹出报警提示框
- 主界面对应数值区域会变成粉红色
- 报警信息会记录到日志中
- 报警记录会保存到数据库

## 常见问题

### Q: 找不到串口怎么办？

A: 请检查：

1. 设备是否正确连接到电脑
2. 设备管理器中是否有该串口
3. 是否被其他程序占用
4. 尝试在 appsettings.json 中修改 PortName

### Q: 如何修改默认密码？

A: 目前版本需要直接在数据库中修改。可以使用 SQLite 管理工具打开 `Data/TempMonitor.db`，更新 Users 表的 Password 字段。

### Q: 数据会无限增长吗？

A: 不会。系统会根据 `DataRetentionDays` 配置项自动清理超过保留天数的数据，默认保留 90 天。

### Q: 支持哪些 Modbus 设备？

A: 理论上支持所有符合 Modbus RTU 协议的温湿度传感器。

### Q: 如何查询特定时间段的数据？

A: 点击主界面的「历史数据」按钮，在弹出的窗口中选择开始和结束时间，点击「查询」即可。

## 开发计划

- [ ] 支持更多 Modbus 寄存器地址配置
- [ ] 添加数据统计和报表功能
- [ ] 支持邮件报警通知
- [ ] 添加更多数据可视化图表
- [ ] 支持多设备同时监控
- [ ] 改进密码管理，支持在界面修改密码
- [ ] 添加数据备份和恢复功能
- [x] 添加历史数据查询界面
- [x] 支持历史数据导出为 Excel 格式

---

**注意**：本系统仅供学习和研究使用，如需用于生产环境，请充分测试并根据实际需求进行调整。
