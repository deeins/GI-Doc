# Fork管理仓库

## 创建分支

在fork上可以创建新分支，此操作对应命令 `git branch XXX`，使用此命令只是创建分支，如果想在创建分支的同时切换分支需要使用 `-b`参数，`git branch -b xxx`，另外创建分支时XXX可以自动创建对应目录，比如 `git branch newBranch/newBranch`代表在newBranch目录创建新分支newBranch，图形界面创建分支直接右键Branchs选项卡选择new branch即可

## 切换分支

切换分支的命令是 `git checkout branchName`，对应到fork上是双击分支，或者右键分支选择 `checkout`

切换分支时会自动更新仓库内容为新分支的内容，比如当前分支有文件1.md，新分支有1.md和2.md，切换到新分支后本地目录内容会更新为1.md和2.md都存在的情况，切回旧分支，2.md文件则会消失

本地分支实际上是存储在.git文件中的，当 `checkout`时会从中提取相关数据

## 获取远端更新状态

如果远端有其他人修改分支，本地并不会同步更新提交情况，此时如果需要获取远端更新情况，需要使用 `git fetch`命令，或者在fork中的顶部工具栏Repository选项卡中选择fetch，更新对应仓库的信息

**注意**如果要切换到远端分支，需要先 `fetch`，再进行 `checkout`操作

获取远端分支的步骤如下：

1. git fetch
2. git branch -r     👉    查看分支
3. git checkout branchName    👉    切换到分支并追踪
4. git pull
