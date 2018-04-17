'use strict';

var mongoose = require('mongoose'),
    Task = mongoose.model('Tasks');

exports.listAllTasks = function(req, res) {
    Task.find({}, function(err, task) {
        if (err) {
             res.status(400).send(err);
        } else {
            res.status(200).json(task);
        }
    });
};

exports.createTask = function(req, res) {
    var newTask = new Task(req.body);
    newTask.save(function(err, task) {
        if (err) {
            res.status(400).send(err);
        } else {
           res.status(200).json(task);
        }
    })
};

exports.readTask = function(req, res) {
    Task.findById(res.params.taskId, function() {
        if (err) {
            res.status(400).send(err);
        } else {
           res.status(200).json(task);
        }
    });
};

exports.updateTask = function(req, res) {
    Task.findOneAndUpdate({id: req.params.taskId}, req.body, {new: true}, function(err, task) {
        if (err) {
            res.status(400).send(err);
        } else {
           res.status(200).json(task);
        }
    });
};

exports.deleteTask = function(req, res) {
    Task.remove({_id:  req.params.taskId}, function(err, task){
        if (err) {
            res.status(400).send(err);
        } else {
           res.status(200).json({message: 'Task successfully deleted'});
        }
    });
};