var AttendanceApp = angular.module("AttendanceApp", ["ngResource", "ngRoute"]).
    config(function ($routeProvider) {
        $routeProvider.
            when('/', { controller: ListCtrl, templateUrl: 'list.html' }).
            when('/new', { controller: CreateCtrl, templateUrl: 'details.html' }).
            when('/edit/:editID', { controller: EditCtrl, templateUrl: 'details.html' }).
            otherwise({ redirectTo: '/' });
    });

AttendanceApp.factory('Attendance', function ($resource) {
    return $resource('/api/Attendance/:id', { id: '@id' }, { update: { method: 'PUT' } });
});

AttendanceApp.factory('Person', function ($resource) {
    return $resource('/api/Person/:id', { id: '@id' }, { update: { method: 'PUT' } });
});

var EditCtrl = function ($scope, $location, $routeParams, Attendance, Person) {

    $scope.search = function () {
        Person.query({ q: $scope.query, sort: $scope.sort_order, desc: $scope.is_desc, offset: $scope.offset, limit: $scope.limit },
            function (data) {
                $scope.more = data.length === 60;
                $scope.People = $scope.People.concat(data);
            });
    };

    $scope.sort = function (col) {
        if ($scope.sort_order === col) {
            $scope.is_desc = !$scope.is_desc;
        } else {
            $scope.sort_order = col;
            $scope.is_desc = false;
        }
        $scope.reset();
    };

    $scope.show_more = function () {
        $scope.offset += $scope.limit;
        $scope.search();
    };

    $scope.has_more = function () {
        return $scope.more;
    }

    $scope.reset = function () {
        $scope.limit = 60;
        $scope.offset = 0;
        $scope.People = [];
        $scope.more = true;
        $scope.search();
    };

    $scope.delete = function () {
        var id = this.Person.ID;
        Person.delete({ id: id }, function () {
            $('#Person_' + id).fadeOut();
        });
    };

    $scope.sort_order = "FullName";
    $scope.is_desc = false;


    $scope.reset();

    $scope.action = "Update";
    var id = $routeParams.editID;
    $scope.item = Attendance.get({ id: id });
    $scope.save = function () {
        Attendance.update({ id: id }, $scope.item, function () {
            $location.path('/');
        })
    };
};

var CreateCtrl = function ($scope, $location, Attendance, Person) {

    $scope.search = function () {
        Person.query({ q: $scope.query, sort: $scope.sort_order, desc: $scope.is_desc, offset: $scope.offset, limit: $scope.limit },
            function (data) {
                $scope.more = data.length === 60;
                $scope.People = $scope.People.concat(data);
            });
    };

    $scope.sort = function (col) {
        if ($scope.sort_order === col) {
            $scope.is_desc = !$scope.is_desc;
        } else {
            $scope.sort_order = col;
            $scope.is_desc = false;
        }
        $scope.reset();
    };

    $scope.show_more = function () {
        $scope.offset += $scope.limit;
        $scope.search();
    };

    $scope.has_more = function () {
        return $scope.more;
    }

    $scope.reset = function () {
        $scope.limit = 60;
        $scope.offset = 0;
        $scope.People = [];
        $scope.more = true;
        $scope.search();
    };

    $scope.delete = function () {
        var id = this.Person.ID;
        Person.delete({ id: id }, function () {
            $('#Person_' + id).fadeOut();
        });
    };

    $scope.sort_order = "FullName";
    $scope.is_desc = false;


    $scope.reset();

    $scope.action = "Add";
    $scope.save = function () {
        Attendance.save($scope.item, function () {
            $location.path('/');
        });
    };
};

var ListCtrl = function ($scope, $location, Attendance) {
    $scope.search = function () {
        Attendance.query({ q: $scope.query, sort: $scope.sort_order, desc: $scope.is_desc, offset: $scope.offset, limit: $scope.limit },
            function (data) {
                $scope.more = data.length === 20;
                $scope.Attendances = $scope.Attendances.concat(data);
            });
    };

    $scope.sort = function (col) {
        if ($scope.sort_order === col) {
            $scope.is_desc = !$scope.is_desc;
        } else {
            $scope.sort_order = col;
            $scope.is_desc = false;
        }
        $scope.reset();
    };

    $scope.show_more = function () {
        $scope.offset += $scope.limit;
        $scope.search();
    };

    $scope.has_more = function () {
        return $scope.more;
    }

    $scope.reset = function () {
        $scope.limit = 20;
        $scope.offset = 0;
        $scope.Attendances = [];
        $scope.more = true;
        $scope.search();
    };

    $scope.delete = function () {
        var id = this.Attendance.ID;
        Attendance.delete({ id: id }, function () {
            $('#Attendance_' + id).fadeOut();
        });
    };

    $scope.sort_order = "TrainingLocation";
    $scope.is_desc = false;


    $scope.reset();
};