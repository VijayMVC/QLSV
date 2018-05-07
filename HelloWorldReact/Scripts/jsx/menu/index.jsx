//var DateTimePicker = require('/Scripts/bootstrap-datetimepicker.js');
//1 comportment lon nhat
var App = React.createClass({
    getInitialState: function () { //Khởi tạo, data=list department
        return { data: [], firsttime: 1 };
    },
    componentWillMount: function () {

        //this.loadData();
        //console.log("bắt đầu");
    },
    componentDidMount: function () {
        if ($("#hidCode").val() != "") {
            this.loadDataW($("#hidCode").val());
        }
    },
    loadSearch: function () {//load database on search
        this.loadDataW();
    },
    loadData: function ()//load list of grid
    {
        this.loadDataW();
    },
    loadDataW: function () {
        $.ajax({
            url: '/menu/getlist',
            dataType: 'json',
            data: {
                keysearch: $.trim($('#keysearch').val()),
                //                page: homeConfig.pageIndex,
                page: 10,
                pageCur: 0//default from server
            },
            success: function (data) {
                if (data.ret >= 0) {
                    this.setState({ data: data.data });
                }
                else {
                    alert("Lỗi không lấy được dữ liệu");
                }
                //AppRendered.loadData();
                //pagination(data.total, function () {
                //    AppRendered.loadData();
                //});
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    setParent: function () {
        $.ajax({
            url: '/menu/getParent',
            dataType: 'json',
            data: null,
            method: 'POST',
            success: function (data) {
                this.setState({ menuParent: data.data });
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },

    backList: function () {
        console.log("Quay lai danh sach");
        $("#CreateModal").modal("hide");
    },

    eventClick: function () {
        //jquery set lai value
        $("#keysearch").val("");
    },

    search: function () {
        this.loadDataW();
    },

    handleNewRowSubmit: function () { //Them moi 1 ban ghi load lai du lieu
        //    this.loadparent();
        this.loadData();
    },
    setEdit: function (title, obj) {
        this.setParent();
        obj = obj || '';//omitted - missed
        if (obj === '') {
            this.clearInput();
            //set the curent parent to parrent
        }
        else {
            //set gia tri cho cac thanh phan giao dien
            $("#MENUID").val(obj.MenuId);
            $("#MENUIDCHA").val(obj.MenuIdCha);
            $("#TITLE").val(obj.Title);
            $("#URL").val(obj.Url);
            $("#TRANGTHAI").val(obj.TrangThai);
            $("#SORT").val(obj.Sort);
        }
        $("#UpdateModal").modal("show");

        $("#titleOption").text(title);
    },
    clearInput: function () {
        $("#MENUID").val('');
        $("#MENUIDCHA").val('');
        $("#TITLE").val('');
        $("#URL").val('');
        $("#TRANGTHAI").value('');
        $("#SORT").val('');
    },
    
    setCreate: function () {
        this.setParent();
        $("#CreateModal").modal("show");
    },
    //phuong thuc quan trong nhat-->render html la ngoai
    render: function () {
        return (
            <div>
                <NewRow menuParent={this.state.menuParent} onRowSubmit={this.handleNewRowSubmit} />
                <EditRow menuParentEdit={this.state.menuParent} onRowSubmit={this.handleEditRowSubmit} />
                <div id="listData">
                    <div style={{ 'margin-bottom': '10px' }}>
                        <div className="col-lg-12 col-md-12" style={{ 'margin-bottom': '10px' }}>
                            <button className="btn btn-sm  btn-primary" id="btnAdd" onClick={() => this.setCreate()} style={{ 'float': 'left' , 'margin-right':'20px'}}>
                                Thêm mới
                                </button>
                            &nbsp;
                             <input type="text" className="form-control" id="keysearch" style={{ 'max-width': '300px', 'float': 'left', 'margin-right': '20px' }} />
                             <input type="button" className="btn btn-sm btn-default" onClick={this.search} style={{ 'float': 'left' }} value="Tìm kiếm" />
                        </div>
                    </div>
                    <ListRow clist={this.state.data} startindex={this.state.startindex} loadData={this.loadData} setEdit={this.setEdit} />
                </div>
            </div>
        );
    }
});
//Tao 1 comportment con để hiển thị danh sách các đơn vị
var ListRow = React.createClass({

    componentWillMount: function () {

    },

    componentDidMount: function () {

    },
    handleRemove: function () {
        $("#ConfirmModal").modal('hide');
        var code = $("#idRemove").val();
        $.ajax({
            url: "/menu/delete",
            data: { id: code }, //truyen id(=CODE) len de xoa
            dataType: 'json',
            success: function (data) {
                if (data.sussess >= 0) {
                    this.loadData(); //xoa xong load lai du lieu
                    //$("#NotificationModal h5").empty().append('Xóa thành công!');
                    //$("#NotificationModal").modal('show');
                } else {
                    //try to waiting
                    $("#NotificationModal h5").empty().append('Không xóa được bản ghi!');
                    $("#NotificationModal").modal('show');
                }
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    loadData: function () {
        //jquery set lai value
        this.props.loadData();
    },
    render: function () {
        var listRow = [];
        var that = this;
        var index = 0;
        //Gọi yêu cầu hiển thi tất cả các department trong danh sách
        if (this.props.clist){
            this.props.clist.forEach(function (rowitem) {
                //child function so that, this does mean thi window not the react object
                index++;
                listRow.push(<RowDetail item={rowitem} index={index} loadData={that.loadData} setEdit={that.props.setEdit} />);
            });
        }
        return (
            <div id="listData">
                <table id="example2" className="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>STT</th>
                            <th>Mã</th>
                            <th>Tên</th>
                            <th>Mã cha</th>
                            <th>Chức năng</th>
                        </tr>
                    </thead>
                    <tbody>{listRow}</tbody>
                </table>

                <div id="paginateBox">
                    <div id="pagination" className="pagination">
                    </div>
                </div>

                <div id="ConfirmModal" className="modal fade" role="dialog">
                    <div className="modal-dialog">
                        <div className="modal-content">
                            <div className="modal-header">
                                <button type="button" className="close" data-dismiss="modal">&times;</button>
                                <h4 className="modal-title">Xác nhận</h4>
                            </div>
                            <div className="modal-body">
                                <input id="idRemove" className="hidden" />
                                <h5>Bạn chắc chắn muốn xóa bản ghi này?</h5>
                            </div>
                            <div className="modal-footer">
                                <button type="button" className="btn btn-danger" onClick={this.handleRemove}>Xác nhận</button>
                                <button type="button" className="btn btn-default" data-dismiss="modal">Hủy</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
});

var RowDetail = React.createClass({
    handleRemove: function () {
        $("#idRemove").val(this.props.item.Id);
        $("#ConfirmModal").modal('show');
        return false;
    },
    showDetail: function () {
        this.props.loadData();
    },
    render: function () {
        return (
            //Hiển thị một phần tử trên danh sách
            <tr>
                <td>{this.props.index}</td>
                <td>{this.props.item.MenuId}</td>
                <td>{this.props.item.Title}</td>
                <td>{this.props.item.MenuIdCha}</td>
                <td>
                    <input type="button" className="btn btn-sm btn-primary" value="Sửa" onClick={() => this.props.setEdit("Sửa bản ghi", this.props.item)} />
                    &nbsp; &nbsp;
                      <input type="button" className="btn btn-sm btn-danger" value="Xóa" onClick={this.handleRemove} />
                </td>
            </tr>
        );
    }
});

var EditRow = React.createClass({

    getInitialState: function () {
        return { lstGroup: [], lstResearchstatus: [] };
    },
    componentWillMount: function () {

    },
    componentDidMount: function () {
        //Thiết lập editor cho các đối tượng
    },
    backList: function () {
        console.log("Quay lai danh sach");
        $("#UpdateModal").modal("hide");

    },
    handleSubmit: function () {
        //Lay gia tri tu cac thanh phan giao dien

        var TITLE = this.refs.NAME.getDOMNode().value;

        var MENUID = this.refs.MENUID.getDOMNode().value;

        var MENUIDCHA = this.refs.MENUIDCHA.getDOMNode().value;

        var URL = this.refs.URL.getDOMNode().value;

        var TRANGTHAI = this.refs.TRANGTHAI.getDOMNode().value;

        var SORT = this.refs.SORT.getDOMNode().value;

        var data = {
            MENUID: MENUID,
            MENUIDCHA: MENUIDCHA,
            TITLE: TITLE,
            URL: URL,
            TRANGTHAI: TRANGTHAI,
            SORT: SORT,
            thetype: $.trim($('#thetype').val()),
            keysearch: $.trim($('#keysearch').val()),
        }
        if (CODEVIEW == "") {
            alert("Chưa nhập mã");
            $("#UpdateModal").modal("show");
            return false;
        } else {
            //Add or edit 1 department
            $.ajax({
                url: "/nation/update",
                type: 'POST',
                data: data,
                dataType: 'json',
                success: function (data) {
                    if (data.sussess >= 0) {
                        this.props.onRowSubmit(); //load lai du lieu
                    }
                }.bind(this),
                error: function (xhr, status, err) {
                    console.error(this.props.url, status, err.toString());
                }.bind(this)
            });

            return false;
        }
    },

    render: function () {
        var listSelect = [];
        var index = 0;
        if (this.props.menuParentEdit) {
            this.props.menuParentEdit.forEach(function (rowitem) {
                //child function so that, this does mean thi window not the react object
                index++;
                listSelect.push(<SelectDetail item={rowitem} index={index} />);
            });
        }
        var inputStyle = { padding: '10px' };
        return (
            <div className="modal fade" id="UpdateModal" role="dialog" data-backdrop="static" data-keyboard="false">
                <div className="modal-dialog">
                    <div className="modal-content ">
                        <div className="modal-header" style={{ 'border-bottom': 'solid 2px #ccc' }}>
                            <button type="button" className="close" data-dismiss="modal"></button>
                            <h4 className="box-title" id="titleOption">Sửa bản ghi</h4>
                        </div>
                        <div className="modal-body modalScroll">
                            <input type="text" className="form-control col-md-8 hidden" ref="CODE" id="CODE" />
                            <form className="form-horizontal">
                                <div className="box-body">
                                    <div className="form-group col-md-12">
                                        <label className="col-md-1 control-label">Mã</label>
                                        <div className="col-md-4">
                                            <input type="text" className="form-control" ref="MENUID" id="MENUID" />
                                        </div>
                                        <label className="col-md-3 control-label">Tên menu</label>
                                        <div className="col-md-4">
                                            <input type="text" className="form-control" ref="TITLE" id="TITLE" />
                                        </div>
                                    </div>
                                    <div className="form-group col-md-12">
                                        <label className="col-md-1 control-label">Url</label>
                                        <div className="col-md-4">
                                            <input type="text" className="form-control" ref="URL" id="URL" />
                                        </div>
                                        <label className="col-md-3 control-label">Menu Cha</label>
                                        <div className="col-md-4">
                                            <select className="form-control" ref="MENUIDCHA" id="MENUIDCHA" onClick={this.setSelect}>
                                                {listSelect}
                                            </select>
                                        </div>
                                    </div>

                                    <div className="form-group col-md-12">
                                        <label className="col-md-1 control-label">STT</label>
                                        <div className="col-md-4">
                                            <input type="number" className="form-control" ref="SORT" id="SORT" />
                                        </div>
                                        <label className="col-md-3 control-label">Trạng thái</label>
                                        <div className="col-md-4">
                                            <select className="form-control" ref="TRANGTHAI" id="TRANGTHAI">
                                                <option value="1">Sử dụng</option>
                                                <option value="0">Không sử dụng</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </form>

                        </div>
                        <div className="modal-footer" style={{ 'border-top': 'solid 2px #ccc' }}>
                            <button className="btn btn-primary" id="cmdSave" onClick={this.handleSubmit}> Lưu </button>
                            &nbsp;
                        <button className="btn btn-danger" id="cmdCancel" data-dismiss="modal">
                                Hủy
                        </button>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
});

var NewRow = React.createClass({
    getInitialState: function () {
        return { lstGroup: [], lstResearchstatus: [] };
    },
    propTypes: {
        value: React.PropTypes.string,
        onChange: React.PropTypes.func
    },

    handleSubmit: function () {
        //Lay gia tri tu cac thanh phan giao dien

        var TITLE = this.refs.CNAME.getDOMNode().value;
        
        var MENUID = this.refs.CMENUID.getDOMNode().value;

        var MENUIDCHA = this.refs.CMENUIDCHA.getDOMNode().value;

        var URL = this.refs.CURL.getDOMNode().value;

        var TRANGTHAI = this.refs.CTRANGTHAI.getDOMNode().value;

        var SORT = this.refs.CSORT.getDOMNode().value;

        var data = {
            MENUID: MENUID,
            MENUIDCHA: MENUIDCHA,
            TITLE: TITLE,
            URL: URL,
            TRANGTHAI: TRANGTHAI,
            SORT: SORT,
            thetype: $.trim($('#thetype').val()),
            keysearch: $.trim($('#keysearch').val()),
        }
        if (MENUID == "") {
            alert("Chưa nhập mã");
            $("#CreateModal").modal("show");
            return false;
        } else {
            //Add or edit 1 department
            $.ajax({
                url: "/menu/create",
                type: 'POST',
                data: data,
                dataType: 'json',
                success: function (data) {
                    if (data.sussess >= 0) {
                        $("#CreateModal").modal("hide");
                        this.props.onRowSubmit(); //load lai du lieu
                        notify.show('Toasty!');
                    }
                }.bind(this),
                error: function (xhr, status, err) {
                    console.error(this.props.url, status, err.toString());
                }.bind(this)
            });

            return false;
        }
    },

    render: function () {
        var listSelect=[];
        var index = 0;
        if (this.props.menuParent) {
            this.props.menuParent.forEach(function (rowitem) {
                //child function so that, this does mean thi window not the react object
                index++;
                listSelect.push(<SelectDetail item={rowitem} index={index}/>);
            });
        }
        var inputStyle = { padding: '10px' };
        return (
            <div className="modal fade" id="CreateModal" role="dialog" data-backdrop="static" data-keyboard="false">
                <div className="modal-dialog">
                    <div className="modal-content ">
                        <div className="modal-header" style={{ 'border-bottom': 'solid 2px #ccc' }}>
                            <button type="button" className="close" data-dismiss="modal"></button>
                            <h4 className="box-title" id="titleOption">Thêm bản ghi mới</h4>
                        </div>
                        <div className="modal-body modalScroll">
                            <input type="text" className="form-control col-md-8 hidden" ref="CODE" id="CODE" />
                            <form className="form-horizontal">
                                <div className="box-body">
                                    <div className="form-group col-md-12">
                                        <label className="col-md-1 control-label">Mã</label>
                                        <div className="col-md-4">
                                            <input type="text" className="form-control" ref="CMENUID" id="CMENUID" />
                                        </div>
                                        <label className="col-md-3 control-label">Tên menu</label>
                                        <div className="col-md-4">
                                            <input type="text" className="form-control" ref="CNAME" id="CNAME" />
                                        </div>
                                    </div>
                                    <div className="form-group col-md-12">
                                        <label className="col-md-1 control-label">Url</label>
                                        <div className="col-md-4">
                                            <input type="text" className="form-control" ref="CURL" id="CURL" />
                                        </div>
                                        <label className="col-md-3 control-label">Menu Cha</label>
                                        <div className="col-md-4">
                                            <select className="form-control" ref="CMENUIDCHA" id="CMENUIDCHA" onClick={this.setSelect}>
                                                {listSelect}
                                            </select>
                                        </div>
                                    </div>

                                    <div className="form-group col-md-12">
                                        <label className="col-md-1 control-label">STT</label>
                                        <div className="col-md-4">
                                            <input type="number" className="form-control" ref="CSORT" id="CSORT" />
                                        </div>
                                        <label className="col-md-3 control-label">Trạng thái</label>
                                        <div className="col-md-4">
                                            <select className="form-control" ref="CTRANGTHAI" id="CTRANGTHAI">
                                                <option value="1">Sử dụng</option>
                                                <option value="0">Không sử dụng</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </form>

                        </div>
                        <div className="modal-footer" style={{ 'border-top': 'solid 2px #ccc' }}>
                            <button className="btn btn-primary" id="cmdSave" onClick={this.handleSubmit}> Lưu </button>
                            &nbsp;
                        <button className="btn btn-danger" id="cmdCancel" data-dismiss="modal">
                                Hủy
                        </button>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
});

var SelectDetail = React.createClass({
    render: function () {
        return (
            <option value={this.props.item.MenuId}>{this.props.item.Title}</option>
        );
    }
});

//Render html vao the co Id = "container"
var AppRendered = React.render(<App />, document.getElementById("content"));//1 comportment lon nhat
