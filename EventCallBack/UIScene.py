# -*- coding: utf-8 -*-
from UIDataNotifier import *
from UIBase import UIBase

class UIScene(UIBase):

    def __init__(self, in_scene_id):
        super(UIScene, self).__init__(in_scene_id)

    @data_listener(OnItemAdded)
    def ui_render_red_point(self, item):
        print 'ui_render_red_point'

    @data_listener(OnItemAdded)
    def ui_render_new_tool(self, item):
        print 'ui_render_new_tool: ' + item

    @data_listener(OnItemAdded)
    def ui_render_avaliable_use_btn(self, item):
        print 'ui_render_avaliable_use_btn'

bag_ui_scene = UIScene(123)