// myprocess.cpp : 定义 DLL 应用程序的导出函数。
//
#include "stdafx.h"
#include "myprocess.h"
#include <HalconC.h>
#include <iostream>
#include <fstream>

#include <vector>
#include <string>
#include <math.h>
#include <stdlib.h>
using namespace std;

void dev_open_window_fit_image (Hobject ho_Image, Htuple hv_Row, Htuple hv_Column, 
    Htuple hv_WidthLimit, Htuple hv_HeightLimit, Htuple *hv_WindowHandle);

void getrightcode(vector<string>& allcode,vector<string>& rightcode);



bool IsSameCode(string string1, string string2)
{
	const char* str1 = string1.c_str();
	const char* str2 = string2.c_str();

	//int i= 0;
	for(int i=0; i<3; i++)
	{
		if(*(str1+i) != *(str2+i))
			return false;
	}

	return true;

}



MYPROCESS_API void _stdcall gray2rbg( void *src, void *des, int srcw, int srch, int desw, int desh,int desstride )
{

	int stepw = srcw/ desw;
	int stepH = srch/desh;

	char* psrc=(char*)src;
	char* pdes = (char*)des;

	for( int i=0;i<desh;i++)
	{

		for( int j=0;j<desw;j++)
		{
			*(pdes+3*j)=*(pdes+3*j+1)=*(pdes+3*j+2)=*(psrc +j*stepw);
		}

		pdes += desstride;
		psrc += srcw*stepH;
	}

}

MYPROCESS_API int _stdcall process(void* pdata, unsigned int w, unsigned int h, char* strval)
{
	//传入图像、传出图像、类型转换
	//目录字符串类型转换
	//字符串的存储和剔除
	//文件输出

	vector<string> allcode;			//保存所有检测条码
	vector<string> rightcode;		//保存提出后正确的
	char* curtxt = strval;

	/* Stack for temporary tuples */
	Htuple   TTemp[100];
	int      SP=0;
	/* Stack for temporary objects */
	Hobject  OTemp[100] = {0};
	int      SPO=0;
	/* Stack for temporary tuple vectors */
	Hvector  TVTemp[100] = {0};
	int      SPTV=0;
	/* Stack for temporary object vectors */
	Hvector  OVTemp[100] = {0};
	int      SPOV=0;

	/* Local iconic variables */
	Hobject  ho_Image, ho_SymbolRegions, ho_Image1;
	Hobject  ho_GrayImage, ho_ImageConverted;

	/* Local control variables */
	Htuple  hv_WindowHandle, hv_BarCodeHandle, hv_BarcodeStrings;
	Htuple  hv_DecodedDataTypes, hv_Number, hv_Index, hv_curstring;
	Htuple  hv_Pointer, hv_Type, hv_Width, hv_Height, hv_PointerInt;

	/* Initialize iconic variables */
	gen_empty_obj(&ho_Image);
	gen_empty_obj(&ho_SymbolRegions);
	gen_empty_obj(&ho_Image1);
	gen_empty_obj(&ho_GrayImage);
	gen_empty_obj(&ho_ImageConverted);

	/* Initialize control variables */
	create_tuple(&hv_WindowHandle,0);
	create_tuple(&hv_BarCodeHandle,0);
	create_tuple(&hv_BarcodeStrings,0);
	create_tuple(&hv_DecodedDataTypes,0);
	create_tuple(&hv_Number,0);
	create_tuple(&hv_Index,0);
	create_tuple(&hv_curstring,0);
	create_tuple(&hv_Pointer,0);
	create_tuple(&hv_Type,0);
	create_tuple(&hv_Width,0);
	create_tuple(&hv_Height,0);
	create_tuple(&hv_PointerInt,0);

	/****************************************************/
	/******************   Begin procedure   *************/
	/****************************************************/

	/********************recognize the Data Bars**************************/

	/****** read the image *******/
	/*read_image (Image, '14.bmp')*/
	clear_obj(ho_Image);
	//***/read_image(&ho_Image, "14.bmp");
	gen_image1(&ho_Image,"byte",w,h,(Hlong)pdata);

	/*dev_open_window_fit_image (Image, 0, 0, -1, -1, WindowHandle)*/
	create_tuple_i(&TTemp[SP++],0);
	create_tuple_i(&TTemp[SP++],0);
	create_tuple_i(&TTemp[SP++],-1);
	create_tuple_i(&TTemp[SP++],-1);
	destroy_tuple(hv_WindowHandle);
	/***/dev_open_window_fit_image(ho_Image, TTemp[SP-4], TTemp[SP-3], TTemp[SP-2], 
		TTemp[SP-1], &hv_WindowHandle);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);


	if (window_stack_is_open())
	{
	Htuple active_win;
	create_tuple_i(&active_win,window_stack_get_active());
	/*dev_set_color ('red')*/
	create_tuple_s(&TTemp[SP++],"red");
	/***/T_set_color(active_win,TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(active_win);
	}
	/********************Pretreatment*******************/

	/********************check the barcode**************************/

	/*create_bar_code_model ([], [], BarCodeHandle)*/
	create_tuple(&TTemp[SP++],0);
	create_tuple(&TTemp[SP++],0);
	destroy_tuple(hv_BarCodeHandle);
	/***/T_create_bar_code_model(TTemp[SP-2], TTemp[SP-1], &hv_BarCodeHandle);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);


	/******set the parameters*******/

	/*set_bar_code_param (BarCodeHandle, 'majority_voting', 'true')*/
	create_tuple_s(&TTemp[SP++],"majority_voting");
	create_tuple_s(&TTemp[SP++],"true");
	/***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);

	/*set_bar_code_param (BarCodeHandle, 'element_size_min', 1.2)*/
	create_tuple_s(&TTemp[SP++],"element_size_min");
	create_tuple_d(&TTemp[SP++],1.2);
	/***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);

	/*set_bar_code_param (BarCodeHandle, 'element_size_max', 2)*/
	create_tuple_s(&TTemp[SP++],"element_size_max");
	create_tuple_i(&TTemp[SP++],2);
	/***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);

	/*set_bar_code_param (BarCodeHandle, 'element_size_variable', 'true')*/
	create_tuple_s(&TTemp[SP++],"element_size_variable");
	create_tuple_s(&TTemp[SP++],"true");
	/***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);

	/*set_bar_code_param (BarCodeHandle, 'num_scanlines', 3)*/
	create_tuple_s(&TTemp[SP++],"num_scanlines");
	create_tuple_i(&TTemp[SP++],3);
	/***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);


	/*find_bar_code (Image, SymbolRegions, BarCodeHandle, 'Code 39', BarcodeStrings)*/
	create_tuple_s(&TTemp[SP++],"Code 39");
	clear_obj(ho_SymbolRegions);
	destroy_tuple(hv_BarcodeStrings);
	/***/T_find_bar_code(ho_Image, &ho_SymbolRegions, hv_BarCodeHandle, TTemp[SP-1], 
		&hv_BarcodeStrings);
	destroy_tuple(TTemp[--SP]);

	/*get_bar_code_result (BarCodeHandle, 'all', 'decoded_types', DecodedDataTypes)*/
	create_tuple_s(&TTemp[SP++],"all");
	create_tuple_s(&TTemp[SP++],"decoded_types");
	destroy_tuple(hv_DecodedDataTypes);
	/***/T_get_bar_code_result(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1], &hv_DecodedDataTypes);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);


	/*count_obj (SymbolRegions, Number)*/
	destroy_tuple(hv_Number);
	/***/T_count_obj(ho_SymbolRegions, &hv_Number);


	/*========== for Index := 0 to Number-1 by 1 ==========*/
	create_tuple_i(&TTemp[SP++],1);
	T_tuple_sub(hv_Number,TTemp[SP-1],&TTemp[SP]);
	destroy_tuple(TTemp[SP-1]);
	TTemp[SP-1]=TTemp[SP];
	create_tuple_i(&TTemp[SP++],1);
	create_tuple_i(&TTemp[SP++],0);
	T_tuple_greater(TTemp[SP-1],TTemp[SP-3],&TTemp[SP]);
	SP++;
	T_tuple_equal(TTemp[SP-2],TTemp[SP-4],&TTemp[SP]);
	if(get_i(TTemp[SP],0) ||
		(!((( get_i(TTemp[SP-1],0)) && (get_d(TTemp[SP-3],0)>0)) ||
		((!get_i(TTemp[SP-1],0)) && (get_d(TTemp[SP-3],0)<0)))))
	{
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP]);
	T_tuple_sub(TTemp[SP-1],TTemp[SP-2],&TTemp[SP]);
	destroy_tuple(hv_Index);
	copy_tuple(TTemp[SP],&hv_Index);
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP]);
	for(;;)
	{
	T_tuple_add(hv_Index,TTemp[SP-1],&TTemp[SP]);
	destroy_tuple(hv_Index);
	copy_tuple(TTemp[SP],&hv_Index);
	destroy_tuple(TTemp[SP]);
	if(get_d(TTemp[SP-1],0)<0)
	T_tuple_less(hv_Index,TTemp[SP-2],&TTemp[SP]);
	else
	T_tuple_greater(hv_Index,TTemp[SP-2],&TTemp[SP]);
	if(get_i(TTemp[SP],0)) break;
	destroy_tuple(TTemp[SP]);
	/*========== for ==========*/

	/***********************/
	/*******循环体内*********/
	/*curstring := BarcodeStrings[Index]*/
	T_tuple_select(hv_BarcodeStrings,hv_Index,&TTemp[SP++]);
	destroy_tuple(hv_curstring);
	hv_curstring=TTemp[--SP];

	//存入vector
	char* curchar = hv_curstring.val.s ;
	string curstring = curchar;
	allcode.push_back(curstring);


	/*转化为string类型，存入Vector,存入txt；*/
	/*******循环体内**********/
	/************************/
	}
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP]);
	}
	else
	{
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP]);
	}/*========== end for ========*/


	/**************************/
	/****处理字符串并输出*******/
	//剔除不需要的条码
	getrightcode(allcode,rightcode);
	//将条码字符串存入txt文件

	ofstream fout;
	fout.open(curtxt,ios::app);

	for(int i=0; i<rightcode.size(); i++)
		fout << rightcode[i] << endl;
	fout.close();
	/**************************/

	

	if (window_stack_is_open())
	{
	Htuple active_win;
	create_tuple_i(&active_win,window_stack_get_active());
	/*dev_display (Image)*/
	/***/T_disp_obj(ho_Image, active_win);
	destroy_tuple(active_win);
	}
	if (window_stack_is_open())
	{
	Htuple active_win;
	create_tuple_i(&active_win,window_stack_get_active());
	/*dev_display (SymbolRegions)*/
	/***/T_disp_obj(ho_SymbolRegions, active_win);
	destroy_tuple(active_win);
	}

	/*dump_window_image (Image1, WindowHandle)*/
	clear_obj(ho_Image1);
	/***/T_dump_window_image(&ho_Image1, hv_WindowHandle);

	/*write_image (Image1, 'bmp', 0, 'processed')*/
	/***/write_image(ho_Image1, "bmp", 0, "processed");

	/*rgb1_to_gray (Image1, GrayImage)*/
	clear_obj(ho_GrayImage);
	/***/rgb1_to_gray(ho_Image1, &ho_GrayImage);

	/*convert_image_type (GrayImage, ImageConverted, 'byte')*/
	clear_obj(ho_ImageConverted);
	/***/convert_image_type(ho_GrayImage, &ho_ImageConverted, "byte");

	/*get_image_pointer1 (ImageConverted, Pointer, Type, Width, Height)*/
	destroy_tuple(hv_Pointer);
	destroy_tuple(hv_Type);
	destroy_tuple(hv_Width);
	destroy_tuple(hv_Height);
	/***/T_get_image_pointer1(ho_ImageConverted, &hv_Pointer, &hv_Type, &hv_Width, 
		&hv_Height);

	/*tuple_int (Pointer, PointerInt)*/
	destroy_tuple(hv_PointerInt);
	/***/T_tuple_int(hv_Pointer, &hv_PointerInt);

	/**memcpy(pdata, (unsigned char*)hv_PointerInt.val.l, h*w);*/

	/*关闭窗口*/
	if (window_stack_is_open())
	{
	close_window(window_stack_pop());
	}
	/*清除句柄*/
	/*clear_bar_code_model (BarCodeHandle)*/
	/***/T_clear_bar_code_model(hv_BarCodeHandle);





	/****************************************************/
	/******************     End procedure   *************/
	/****************************************************/

	/* Clear temporary tuple stack */
	while (SP > 0)
	destroy_tuple(TTemp[--SP]);
	/* Clear temporary object stack */
	while (SPO > 0)
	clear_obj(OTemp[--SPO]);
	/* Clear temporary tuple vectors stack*/
	while (SPTV > 0)
	V_destroy_vector(TVTemp[--SPTV]);
	/* Clear temporary object vectors stack */
	while (SPOV > 0)
	V_destroy_vector(OVTemp[--SPOV]);
	/* Clear local iconic variables */
	clear_obj(ho_Image);
	clear_obj(ho_SymbolRegions);
	clear_obj(ho_Image1);
	clear_obj(ho_GrayImage);
	clear_obj(ho_ImageConverted);

	/* Clear local control variables */
	destroy_tuple(hv_WindowHandle);
	destroy_tuple(hv_BarCodeHandle);
	destroy_tuple(hv_BarcodeStrings);
	destroy_tuple(hv_DecodedDataTypes);
	destroy_tuple(hv_Number);
	destroy_tuple(hv_Index);
	destroy_tuple(hv_curstring);
	destroy_tuple(hv_Pointer);
	destroy_tuple(hv_Type);
	destroy_tuple(hv_Width);
	destroy_tuple(hv_Height);
	destroy_tuple(hv_PointerInt);

	return rightcode.size();
}

MYPROCESS_API int _stdcall process1(void* pdata, unsigned int w, unsigned int h, char* strval)
{
	

	//传入图像、传出图像、类型转换
	//目录字符串类型转换
	//字符串的存储和剔除
	//文件输出

	vector<string> allcode;			//保存所有检测条码
	vector<string> rightcode;		//保存提出后正确的
	char* curtxt = strval;
	/* Stack for temporary tuples */
	Htuple   TTemp[100];
	int      SP=0;
	/* Stack for temporary objects */
	Hobject  OTemp[100] = {0};
	int      SPO=0;
	/* Stack for temporary tuple vectors */
	Hvector  TVTemp[100] = {0};
	int      SPTV=0;
	/* Stack for temporary object vectors */
	Hvector  OVTemp[100] = {0};
	int      SPOV=0;

	/* Local iconic variables */
	Hobject  ho_Image, ho_SymbolRegions, ho_Image1;
	Hobject  ho_GrayImage, ho_ImageConverted;

	/* Local control variables */
	Htuple  hv_WindowHandle, hv_BarCodeHandle, hv_BarcodeStrings;
	Htuple  hv_DecodedDataTypes, hv_Number, hv_Index, hv_curstring;
	Htuple  hv_Pointer, hv_Type, hv_Width, hv_Height, hv_PointerInt;

	/* Initialize iconic variables */
	gen_empty_obj(&ho_Image);
	gen_empty_obj(&ho_SymbolRegions);
	gen_empty_obj(&ho_Image1);
	gen_empty_obj(&ho_GrayImage);
	gen_empty_obj(&ho_ImageConverted);

	/* Initialize control variables */
	create_tuple(&hv_WindowHandle,0);
	create_tuple(&hv_BarCodeHandle,0);
	create_tuple(&hv_BarcodeStrings,0);
	create_tuple(&hv_DecodedDataTypes,0);
	create_tuple(&hv_Number,0);
	create_tuple(&hv_Index,0);
	create_tuple(&hv_curstring,0);
	create_tuple(&hv_Pointer,0);
	create_tuple(&hv_Type,0);
	create_tuple(&hv_Width,0);
	create_tuple(&hv_Height,0);
	create_tuple(&hv_PointerInt,0);

	/****************************************************/
	/******************   Begin procedure   *************/
	/****************************************************/

	/********************recognize the Data Bars**************************/

	/****** read the image *******/
	/*read_image (Image, '14.bmp')*/
	clear_obj(ho_Image);
	//***/read_image(&ho_Image, "14.bmp");
	gen_image1(&ho_Image,"byte",w,h,(Hlong)pdata);

	/*dev_open_window_fit_image (Image, 0, 0, -1, -1, WindowHandle)*/
	create_tuple_i(&TTemp[SP++],0);
	create_tuple_i(&TTemp[SP++],0);
	create_tuple_i(&TTemp[SP++],-1);
	create_tuple_i(&TTemp[SP++],-1);
	destroy_tuple(hv_WindowHandle);
	/***/dev_open_window_fit_image(ho_Image, TTemp[SP-4], TTemp[SP-3], TTemp[SP-2], 
		TTemp[SP-1], &hv_WindowHandle);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);


	if (window_stack_is_open())
	{
	Htuple active_win;
	create_tuple_i(&active_win,window_stack_get_active());
	/*dev_set_color ('red')*/
	create_tuple_s(&TTemp[SP++],"red");
	/***/T_set_color(active_win,TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(active_win);
	}
	/********************Pretreatment*******************/

	/********************check the barcode**************************/

	/*create_bar_code_model ([], [], BarCodeHandle)*/
	create_tuple(&TTemp[SP++],0);
	create_tuple(&TTemp[SP++],0);
	destroy_tuple(hv_BarCodeHandle);
	/***/T_create_bar_code_model(TTemp[SP-2], TTemp[SP-1], &hv_BarCodeHandle);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);


	/******set the parameters*******/

	/*set_bar_code_param (BarCodeHandle, 'majority_voting', 'true')*/
	create_tuple_s(&TTemp[SP++],"majority_voting");
	create_tuple_s(&TTemp[SP++],"true");
	/***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);

	/*set_bar_code_param (BarCodeHandle, 'element_size_min', 1.2)*/
	create_tuple_s(&TTemp[SP++],"element_size_min");
	create_tuple_d(&TTemp[SP++],1.2);
	/***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);

	/*set_bar_code_param (BarCodeHandle, 'element_size_max', 2)*/
	create_tuple_s(&TTemp[SP++],"element_size_max");
	create_tuple_i(&TTemp[SP++],2);
	/***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);

	/*set_bar_code_param (BarCodeHandle, 'element_size_variable', 'true')*/
	create_tuple_s(&TTemp[SP++],"element_size_variable");
	create_tuple_s(&TTemp[SP++],"true");
	/***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);

	/*set_bar_code_param (BarCodeHandle, 'num_scanlines', 3)*/
	create_tuple_s(&TTemp[SP++],"num_scanlines");
	create_tuple_i(&TTemp[SP++],3);
	/***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);


	/*find_bar_code (Image, SymbolRegions, BarCodeHandle, 'Code 39', BarcodeStrings)*/
	create_tuple_s(&TTemp[SP++],"Code 39");
	clear_obj(ho_SymbolRegions);
	destroy_tuple(hv_BarcodeStrings);
	/***/T_find_bar_code(ho_Image, &ho_SymbolRegions, hv_BarCodeHandle, TTemp[SP-1], 
		&hv_BarcodeStrings);
	destroy_tuple(TTemp[--SP]);

	/*get_bar_code_result (BarCodeHandle, 'all', 'decoded_types', DecodedDataTypes)*/
	create_tuple_s(&TTemp[SP++],"all");
	create_tuple_s(&TTemp[SP++],"decoded_types");
	destroy_tuple(hv_DecodedDataTypes);
	/***/T_get_bar_code_result(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1], &hv_DecodedDataTypes);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);


	/*count_obj (SymbolRegions, Number)*/
	destroy_tuple(hv_Number);
	/***/T_count_obj(ho_SymbolRegions, &hv_Number);


	/*========== for Index := 0 to Number-1 by 1 ==========*/
	create_tuple_i(&TTemp[SP++],1);
	T_tuple_sub(hv_Number,TTemp[SP-1],&TTemp[SP]);
	destroy_tuple(TTemp[SP-1]);
	TTemp[SP-1]=TTemp[SP];
	create_tuple_i(&TTemp[SP++],1);
	create_tuple_i(&TTemp[SP++],0);
	T_tuple_greater(TTemp[SP-1],TTemp[SP-3],&TTemp[SP]);
	SP++;
	T_tuple_equal(TTemp[SP-2],TTemp[SP-4],&TTemp[SP]);
	if(get_i(TTemp[SP],0) ||
		(!((( get_i(TTemp[SP-1],0)) && (get_d(TTemp[SP-3],0)>0)) ||
		((!get_i(TTemp[SP-1],0)) && (get_d(TTemp[SP-3],0)<0)))))
	{
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP]);
	T_tuple_sub(TTemp[SP-1],TTemp[SP-2],&TTemp[SP]);
	destroy_tuple(hv_Index);
	copy_tuple(TTemp[SP],&hv_Index);
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP]);
	for(;;)
	{
	T_tuple_add(hv_Index,TTemp[SP-1],&TTemp[SP]);
	destroy_tuple(hv_Index);
	copy_tuple(TTemp[SP],&hv_Index);
	destroy_tuple(TTemp[SP]);
	if(get_d(TTemp[SP-1],0)<0)
	T_tuple_less(hv_Index,TTemp[SP-2],&TTemp[SP]);
	else
	T_tuple_greater(hv_Index,TTemp[SP-2],&TTemp[SP]);
	if(get_i(TTemp[SP],0)) break;
	destroy_tuple(TTemp[SP]);
	/*========== for ==========*/

	/***********************/
	/*******循环体内*********/
	/*curstring := BarcodeStrings[Index]*/
	T_tuple_select(hv_BarcodeStrings,hv_Index,&TTemp[SP++]);
	destroy_tuple(hv_curstring);
	hv_curstring=TTemp[--SP];

	/*转化为string类型，存入Vector,存入txt；*/
	
	//存入vector
	char* curchar = hv_curstring.val.s ;
	string curstring = curchar;
	allcode.push_back(curstring);


	
	/*******循环体内**********/
	/************************/
	}
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP]);
	}
	else
	{
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP--]);
	destroy_tuple(TTemp[SP]);
	}/*========== end for ========*/




	/**************************/
	/****处理字符串并输出*******/
	//剔除不需要的条码
	getrightcode(allcode,rightcode);
	//将条码字符串存入txt文件

	ofstream fout;
	fout.open(curtxt,ios::app);
	for(int i=0; i<rightcode.size(); i++)
		fout << rightcode[i] << endl;
	fout.close();

	/**************************/

	if (window_stack_is_open())
	{
	Htuple active_win;
	create_tuple_i(&active_win,window_stack_get_active());
	/*dev_display (Image)*/
	/***/T_disp_obj(ho_Image, active_win);
	destroy_tuple(active_win);
	}
	if (window_stack_is_open())
	{
	Htuple active_win;
	create_tuple_i(&active_win,window_stack_get_active());
	/*dev_display (SymbolRegions)*/
	/***/T_disp_obj(ho_SymbolRegions, active_win);
	destroy_tuple(active_win);
	}

	/*dump_window_image (Image1, WindowHandle)*/
	clear_obj(ho_Image1);
	/***/T_dump_window_image(&ho_Image1, hv_WindowHandle);

	/*write_image (Image1, 'jpeg', 0, 'saved3')*/
	/*rgb1_to_gray (Image1, GrayImage)*/
	clear_obj(ho_GrayImage);
	/***/rgb1_to_gray(ho_Image1, &ho_GrayImage);

	/*convert_image_type (GrayImage, ImageConverted, 'byte')*/
	clear_obj(ho_ImageConverted);
	/***/convert_image_type(ho_GrayImage, &ho_ImageConverted, "byte");

	/*get_image_pointer1 (ImageConverted, Pointer, Type, Width, Height)*/
	destroy_tuple(hv_Pointer);
	destroy_tuple(hv_Type);
	destroy_tuple(hv_Width);
	destroy_tuple(hv_Height);
	/***/T_get_image_pointer1(ho_ImageConverted, &hv_Pointer, &hv_Type, &hv_Width, 
		&hv_Height);

	/*tuple_int (Pointer, PointerInt)*/
	destroy_tuple(hv_PointerInt);
	/***/T_tuple_int(hv_Pointer, &hv_PointerInt);


	if (window_stack_is_open())
	{
	close_window(window_stack_pop());
	}
	/*clear_bar_code_model (BarCodeHandle)*/
	/***/T_clear_bar_code_model(hv_BarCodeHandle);

	//将图像再传出
	//memcpy(pdata, (unsigned char*)hv_PointerInt.val.l, h*w);





	/****************************************************/
	/******************     End procedure   *************/
	/****************************************************/

	/* Clear temporary tuple stack */
	while (SP > 0)
	destroy_tuple(TTemp[--SP]);
	/* Clear temporary object stack */
	while (SPO > 0)
	clear_obj(OTemp[--SPO]);
	/* Clear temporary tuple vectors stack*/
	while (SPTV > 0)
	V_destroy_vector(TVTemp[--SPTV]);
	/* Clear temporary object vectors stack */
	while (SPOV > 0)
	V_destroy_vector(OVTemp[--SPOV]);
	/* Clear local iconic variables */
	clear_obj(ho_Image);
	clear_obj(ho_SymbolRegions);
	clear_obj(ho_Image1);
	clear_obj(ho_GrayImage);
	clear_obj(ho_ImageConverted);

	/* Clear local control variables */
	destroy_tuple(hv_WindowHandle);
	destroy_tuple(hv_BarCodeHandle);
	destroy_tuple(hv_BarcodeStrings);
	destroy_tuple(hv_DecodedDataTypes);
	destroy_tuple(hv_Number);
	destroy_tuple(hv_Index);
	destroy_tuple(hv_curstring);
	destroy_tuple(hv_Pointer);
	destroy_tuple(hv_Type);
	destroy_tuple(hv_Width);
	destroy_tuple(hv_Height);
	destroy_tuple(hv_PointerInt);

	return 0;
}

MYPROCESS_API void _stdcall detectcode()
{
	
	  /* Stack for temporary tuples */
  Htuple   TTemp[100];
  int      SP=0;
  /* Stack for temporary objects */
  Hobject  OTemp[100] = {0};
  int      SPO=0;
  /* Stack for temporary tuple vectors */
  Hvector  TVTemp[100] = {0};
  int      SPTV=0;
  /* Stack for temporary object vectors */
  Hvector  OVTemp[100] = {0};
  int      SPOV=0;

  /* Local iconic variables */
  Hobject  ho_Image, ho_SymbolRegions;

  /* Local control variables */
  Htuple  hv_BarCodeHandle, hv_BarcodeStrings, hv_DecodedDataTypes;
  Htuple  hv_Number, hv_Index, hv_curstring;

  /* Initialize iconic variables */
  gen_empty_obj(&ho_Image);
  gen_empty_obj(&ho_SymbolRegions);

  /* Initialize control variables */
  create_tuple(&hv_BarCodeHandle,0);
  create_tuple(&hv_BarcodeStrings,0);
  create_tuple(&hv_DecodedDataTypes,0);
  create_tuple(&hv_Number,0);
  create_tuple(&hv_Index,0);
  create_tuple(&hv_curstring,0);

  /****************************************************/
  /******************   Begin procedure   *************/
  /****************************************************/

  /********************recognize the Data Bars**************************/

  /****** read the image *******/

  /*read_image (Image, '14.bmp')*/
  clear_obj(ho_Image);
  /***/read_image(&ho_Image, "14.bmp");

  /*dev_open_window_fit_image (Image, 0, 0, -1, -1, WindowHandle)*/

  /*dev_set_color ('red')*/
  /********************Pretreatment*******************/

  /********************check the barcode**************************/

  /*create_bar_code_model ([], [], BarCodeHandle)*/
  create_tuple(&TTemp[SP++],0);
  create_tuple(&TTemp[SP++],0);
  destroy_tuple(hv_BarCodeHandle);
  /***/T_create_bar_code_model(TTemp[SP-2], TTemp[SP-1], &hv_BarCodeHandle);
  destroy_tuple(TTemp[--SP]);
  destroy_tuple(TTemp[--SP]);


  /******set the parameters*******/

  /*set_bar_code_param (BarCodeHandle, 'majority_voting', 'true')*/
  create_tuple_s(&TTemp[SP++],"majority_voting");
  create_tuple_s(&TTemp[SP++],"true");
  /***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
  destroy_tuple(TTemp[--SP]);
  destroy_tuple(TTemp[--SP]);

  /*set_bar_code_param (BarCodeHandle, 'element_size_min', 1.2)*/
  create_tuple_s(&TTemp[SP++],"element_size_min");
  create_tuple_d(&TTemp[SP++],1.2);
  /***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
  destroy_tuple(TTemp[--SP]);
  destroy_tuple(TTemp[--SP]);

  /*set_bar_code_param (BarCodeHandle, 'element_size_max', 2)*/
  create_tuple_s(&TTemp[SP++],"element_size_max");
  create_tuple_i(&TTemp[SP++],2);
  /***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
  destroy_tuple(TTemp[--SP]);
  destroy_tuple(TTemp[--SP]);

  /*set_bar_code_param (BarCodeHandle, 'element_size_variable', 'true')*/
  create_tuple_s(&TTemp[SP++],"element_size_variable");
  create_tuple_s(&TTemp[SP++],"true");
  /***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
  destroy_tuple(TTemp[--SP]);
  destroy_tuple(TTemp[--SP]);

  /*set_bar_code_param (BarCodeHandle, 'num_scanlines', 3)*/
  create_tuple_s(&TTemp[SP++],"num_scanlines");
  create_tuple_i(&TTemp[SP++],3);
  /***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
  destroy_tuple(TTemp[--SP]);
  destroy_tuple(TTemp[--SP]);


  /*find_bar_code (Image, SymbolRegions, BarCodeHandle, 'Code 39', BarcodeStrings)*/
  create_tuple_s(&TTemp[SP++],"Code 39");
  clear_obj(ho_SymbolRegions);
  destroy_tuple(hv_BarcodeStrings);
  /***/T_find_bar_code(ho_Image, &ho_SymbolRegions, hv_BarCodeHandle, TTemp[SP-1], 
      &hv_BarcodeStrings);
  destroy_tuple(TTemp[--SP]);

  /*get_bar_code_result (BarCodeHandle, 'all', 'decoded_types', DecodedDataTypes)*/
  create_tuple_s(&TTemp[SP++],"all");
  create_tuple_s(&TTemp[SP++],"decoded_types");
  destroy_tuple(hv_DecodedDataTypes);
  /***/T_get_bar_code_result(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1], &hv_DecodedDataTypes);
  destroy_tuple(TTemp[--SP]);
  destroy_tuple(TTemp[--SP]);



  /*count_obj (SymbolRegions, Number)*/
  destroy_tuple(hv_Number);
  /***/T_count_obj(ho_SymbolRegions, &hv_Number);


  /****/
  /***如果Number小于3，不保存，大于3，开始比较*/
  vector<string> allcode;
  vector<string> rightcode;
  /****/

  /*========== for Index := 0 to Number-1 by 1 ==========*/
  create_tuple_i(&TTemp[SP++],1);
  T_tuple_sub(hv_Number,TTemp[SP-1],&TTemp[SP]);
  destroy_tuple(TTemp[SP-1]);
  TTemp[SP-1]=TTemp[SP];
  create_tuple_i(&TTemp[SP++],1);
  create_tuple_i(&TTemp[SP++],0);
  T_tuple_greater(TTemp[SP-1],TTemp[SP-3],&TTemp[SP]);
  SP++;
  T_tuple_equal(TTemp[SP-2],TTemp[SP-4],&TTemp[SP]);
  if(get_i(TTemp[SP],0) ||
     (!((( get_i(TTemp[SP-1],0)) && (get_d(TTemp[SP-3],0)>0)) ||
        ((!get_i(TTemp[SP-1],0)) && (get_d(TTemp[SP-3],0)<0)))))
  {
   destroy_tuple(TTemp[SP--]);
   destroy_tuple(TTemp[SP]);
   T_tuple_sub(TTemp[SP-1],TTemp[SP-2],&TTemp[SP]);
   destroy_tuple(hv_Index);
   copy_tuple(TTemp[SP],&hv_Index);
   destroy_tuple(TTemp[SP--]);
   destroy_tuple(TTemp[SP]);
   for(;;)
   {
   T_tuple_add(hv_Index,TTemp[SP-1],&TTemp[SP]);
   destroy_tuple(hv_Index);
   copy_tuple(TTemp[SP],&hv_Index);
   destroy_tuple(TTemp[SP]);
   if(get_d(TTemp[SP-1],0)<0)
    T_tuple_less(hv_Index,TTemp[SP-2],&TTemp[SP]);
   else
    T_tuple_greater(hv_Index,TTemp[SP-2],&TTemp[SP]);
   if(get_i(TTemp[SP],0)) break;
   destroy_tuple(TTemp[SP]);
   /*========== for ==========*/

    /***************/
    /*curstring := BarcodeStrings[Index]*/
    T_tuple_select(hv_BarcodeStrings,hv_Index,&TTemp[SP++]);
    destroy_tuple(hv_curstring);
    hv_curstring=TTemp[--SP];

	char* curchar = hv_curstring.val.s ;
	string curstring = curchar;
	allcode.push_back(curstring);
	

    /*****************/
   }
   destroy_tuple(TTemp[SP--]);
   destroy_tuple(TTemp[SP--]);
   destroy_tuple(TTemp[SP]);
  }
  else
  {
   destroy_tuple(TTemp[SP--]);
   destroy_tuple(TTemp[SP--]);
   destroy_tuple(TTemp[SP--]);
   destroy_tuple(TTemp[SP--]);
   destroy_tuple(TTemp[SP]);
  }/*========== end for ========*/


  getrightcode(allcode,rightcode);



  /*dev_display (Image)*/
  /*dev_display (SymbolRegions)*/

  /*dump_window_image (Image1, WindowHandle)*/
  /*write_image (Image1, 'jpeg', 0, 'saved2')*/

  /*dev_close_window ()*/
  /*clear_bar_code_model (BarCodeHandle)*/
  /***/T_clear_bar_code_model(hv_BarCodeHandle);

  //compare all the code.
	char* curtxt = "../../1.txt";
	ofstream fout;
	fout.open(curtxt,ios::app);
	for(int i=0; i<rightcode.size(); i++)
		fout << rightcode[i] << endl;
	fout.close();



  /****************************************************/
  /******************     End procedure   *************/
  /****************************************************/

  /* Clear temporary tuple stack */
  while (SP > 0)
    destroy_tuple(TTemp[--SP]);
  /* Clear temporary object stack */
  while (SPO > 0)
    clear_obj(OTemp[--SPO]);
  /* Clear temporary tuple vectors stack*/
  while (SPTV > 0)
    V_destroy_vector(TVTemp[--SPTV]);
  /* Clear temporary object vectors stack */
  while (SPOV > 0)
    V_destroy_vector(OVTemp[--SPOV]);
  /* Clear local iconic variables */
  clear_obj(ho_Image);
  clear_obj(ho_SymbolRegions);

  /* Clear local control variables */
  destroy_tuple(hv_BarCodeHandle);
  destroy_tuple(hv_BarcodeStrings);
  destroy_tuple(hv_DecodedDataTypes);
  destroy_tuple(hv_Number);
  destroy_tuple(hv_Index);
  destroy_tuple(hv_curstring);

}

MYPROCESS_API int _stdcall findcode(void* pdata, unsigned int w, unsigned int h)
{
	/* Stack for temporary tuples */
	Htuple   TTemp[100];
	int      SP=0;
	/* Stack for temporary objects */
	Hobject  OTemp[100] = {0};
	int      SPO=0;
	/* Stack for temporary tuple vectors */
	Hvector  TVTemp[100] = {0};
	int      SPTV=0;
	/* Stack for temporary object vectors */
	Hvector  OVTemp[100] = {0};
	int      SPOV=0;

	/* Local iconic variables */
	Hobject  ho_Image, ho_SymbolRegions, ho_Image1;

	/* Local control variables */
	Htuple  hv_WindowHandle, hv_BarCodeHandle, hv_BarcodeStrings;
	Htuple  hv_DecodedDataTypes;

	/* Initialize iconic variables */
	gen_empty_obj(&ho_Image);
	gen_empty_obj(&ho_SymbolRegions);
	gen_empty_obj(&ho_Image1);

	/* Initialize control variables */
	create_tuple(&hv_WindowHandle,0);
	create_tuple(&hv_BarCodeHandle,0);
	create_tuple(&hv_BarcodeStrings,0);
	create_tuple(&hv_DecodedDataTypes,0);

	/****************************************************/
	/******************   Begin procedure   *************/
	/****************************************************/

	/********************recognize the Data Bars**************************/

	/****** read the image *******/

	/*read_image (Image, 'Image__2017-12-25__20-01-48.bmp')*/
	clear_obj(ho_Image);
	//***/read_image(&ho_Image, "1226.bmp");
	gen_image1(&ho_Image,"byte",w,h,(Hlong)pdata);


	/*dev_open_window_fit_image (Image, 0, 0, -1, -1, WindowHandle)*/
	create_tuple_i(&TTemp[SP++],0);
	create_tuple_i(&TTemp[SP++],0);
	create_tuple_i(&TTemp[SP++],-1);
	create_tuple_i(&TTemp[SP++],-1);
	destroy_tuple(hv_WindowHandle);
	/***/dev_open_window_fit_image(ho_Image, TTemp[SP-4], TTemp[SP-3], TTemp[SP-2], 
		TTemp[SP-1], &hv_WindowHandle);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);


	if (window_stack_is_open())
	{
	Htuple active_win;
	create_tuple_i(&active_win,window_stack_get_active());
	/*dev_set_color ('red')*/
	create_tuple_s(&TTemp[SP++],"red");
	/***/T_set_color(active_win,TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(active_win);
	}
	/********************Pretreatment*******************/


	/********************check the barcode**************************/

	/*create_bar_code_model ([], [], BarCodeHandle)*/
	create_tuple(&TTemp[SP++],0);
	create_tuple(&TTemp[SP++],0);
	destroy_tuple(hv_BarCodeHandle);
	/***/T_create_bar_code_model(TTemp[SP-2], TTemp[SP-1], &hv_BarCodeHandle);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);


	/******set the parameters*******/

	/*set_bar_code_param (BarCodeHandle, 'majority_voting', 'true')*/
	create_tuple_s(&TTemp[SP++],"majority_voting");
	create_tuple_s(&TTemp[SP++],"true");
	/***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);

	/*set_bar_code_param (BarCodeHandle, 'element_size_min', 1.2)*/
	create_tuple_s(&TTemp[SP++],"element_size_min");
	create_tuple_d(&TTemp[SP++],1.2);
	/***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);

	/*set_bar_code_param (BarCodeHandle, 'element_size_max', 2)*/
	create_tuple_s(&TTemp[SP++],"element_size_max");
	create_tuple_i(&TTemp[SP++],2);
	/***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);

	/*set_bar_code_param (BarCodeHandle, 'element_size_variable', 'true')*/
	create_tuple_s(&TTemp[SP++],"element_size_variable");
	create_tuple_s(&TTemp[SP++],"true");
	/***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);

	/*set_bar_code_param (BarCodeHandle, 'num_scanlines', 3)*/
	create_tuple_s(&TTemp[SP++],"num_scanlines");
	create_tuple_i(&TTemp[SP++],3);
	/***/T_set_bar_code_param(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1]);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);


	/*find_bar_code (Image, SymbolRegions, BarCodeHandle, 'Code 39', BarcodeStrings)*/
	create_tuple_s(&TTemp[SP++],"Code 39");
	clear_obj(ho_SymbolRegions);
	destroy_tuple(hv_BarcodeStrings);
	/***/T_find_bar_code(ho_Image, &ho_SymbolRegions, hv_BarCodeHandle, TTemp[SP-1], 
		&hv_BarcodeStrings);
	destroy_tuple(TTemp[--SP]);

	/*get_bar_code_result (BarCodeHandle, 'all', 'decoded_types', DecodedDataTypes)*/
	create_tuple_s(&TTemp[SP++],"all");
	create_tuple_s(&TTemp[SP++],"decoded_types");
	destroy_tuple(hv_DecodedDataTypes);
	/***/T_get_bar_code_result(hv_BarCodeHandle, TTemp[SP-2], TTemp[SP-1], &hv_DecodedDataTypes);
	destroy_tuple(TTemp[--SP]);
	destroy_tuple(TTemp[--SP]);



	if (window_stack_is_open())
	{
	Htuple active_win;
	create_tuple_i(&active_win,window_stack_get_active());
	/*dev_display (Image)*/
	/***/T_disp_obj(ho_Image, active_win);
	destroy_tuple(active_win);
	}
	if (window_stack_is_open())
	{
	Htuple active_win;
	create_tuple_i(&active_win,window_stack_get_active());
	/*dev_display (SymbolRegions)*/
	/***/T_disp_obj(ho_SymbolRegions, active_win);
	destroy_tuple(active_win);
	}

	/*dump_window_image (Image1, WindowHandle)*/
	clear_obj(ho_Image1);
	/***/T_dump_window_image(&ho_Image1, hv_WindowHandle);

	/*write_image (Image1, 'jpeg', 0, 'saved2')*/
	/***/write_image(ho_Image1, "jpeg", 0, "save1226");

	//关闭窗口//
	if (window_stack_is_open())
	{
		close_window(window_stack_pop());
	}
	/*clear_bar_code_model (BarCodeHandle)*/
	/***/T_clear_bar_code_model(hv_BarCodeHandle);





	/****************************************************/
	/******************     End procedure   *************/
	/****************************************************/

	/* Clear temporary tuple stack */
	while (SP > 0)
	destroy_tuple(TTemp[--SP]);
	/* Clear temporary object stack */
	while (SPO > 0)
	clear_obj(OTemp[--SPO]);
	/* Clear temporary tuple vectors stack*/
	while (SPTV > 0)
	V_destroy_vector(TVTemp[--SPTV]);
	/* Clear temporary object vectors stack */
	while (SPOV > 0)
	V_destroy_vector(OVTemp[--SPOV]);
	/* Clear local iconic variables */
	clear_obj(ho_Image);
	clear_obj(ho_SymbolRegions);
	clear_obj(ho_Image1);

	/* Clear local control variables */
	destroy_tuple(hv_WindowHandle);
	destroy_tuple(hv_BarCodeHandle);
	destroy_tuple(hv_BarcodeStrings);
	destroy_tuple(hv_DecodedDataTypes);


	return 1226;


}

MYPROCESS_API int __stdcall teststring(char* val)
{
	*val = 'W';
	string curstring = val;
	char* curtxt = "1.txt";
	ofstream fout;
	fout.open(curtxt);
	fout << curstring << endl;
	fout.close();
	return 99;
}


void getrightcode(vector<string>& allcode,vector<string>& rightcode)
{
	int allcodenum = allcode.size();
	for(int i=0; i<allcodenum; i++)
	{
		string curstr = allcode[i];
		int similarity = 0;
		bool isright = false;
		for(int j=0; j<allcodenum; j++)
		{
			string thisstr = allcode[j];
			if(IsSameCode(curstr, thisstr))
			{
				similarity += 1;
				
			}	
			if( j>=6 && similarity >=3)
			{
				isright = true;
				break;
			}
			if( j>=6 && similarity <3)
			{
				isright = false;
				break;
			}
		
		}

		if(isright == true) 
		rightcode.push_back(curstr);


	}



}


void dev_open_window_fit_image (Hobject ho_Image, Htuple hv_Row, Htuple hv_Column, 
		Htuple hv_WidthLimit, Htuple hv_HeightLimit, Htuple *hv_WindowHandle)
{
	  /* Stack for temporary tuples */
	  Htuple   TTemp[100];
	  int      SP=0;
	  /* Stack for temporary objects */
	  Hobject  OTemp[100] = {0};
	  int      SPO=0;
	  /* Stack for temporary tuple vectors */
	  Hvector  TVTemp[100] = {0};
	  int      SPTV=0;
	  /* Stack for temporary object vectors */
	  Hvector  OVTemp[100] = {0};
	  int      SPOV=0;

	  /* Local iconic variables */

	  /* Local control variables */
	  Htuple  hv_MinWidth, hv_MaxWidth, hv_MinHeight;
	  Htuple  hv_MaxHeight, hv_ResizeFactor, hv_ImageWidth, hv_ImageHeight;
	  Htuple  hv_TempWidth, hv_TempHeight, hv_WindowWidth, hv_WindowHeight;

	  /* Initialize control variables */
	  create_tuple(&hv_MinWidth,0);
	  create_tuple(&hv_MaxWidth,0);
	  create_tuple(&hv_MinHeight,0);
	  create_tuple(&hv_MaxHeight,0);
	  create_tuple(&hv_ResizeFactor,0);
	  create_tuple(&hv_ImageWidth,0);
	  create_tuple(&hv_ImageHeight,0);
	  create_tuple(&hv_TempWidth,0);
	  create_tuple(&hv_TempHeight,0);
	  create_tuple(&hv_WindowWidth,0);
	  create_tuple(&hv_WindowHeight,0);
	  create_tuple(&(*hv_WindowHandle),0);

	  /****************************************************/
	  /******************   Begin procedure   *************/
	  /****************************************************/

	  /*This procedure opens a new graphics window and adjusts the size*/
	  /*such that it fits into the limits specified by WidthLimit*/
	  /*and HeightLimit, but also maintains the correct image aspect ratio.*/
	  /**/
	  /*If it is impossible to match the minimum and maximum extent requirements*/
	  /*at the same time (f.e. if the image is very long but narrow),*/
	  /*the maximum value gets a higher priority,*/
	  /**/
	  /*Parse input tuple WidthLimit*/
	  /*========== if (|WidthLimit| == 0 or WidthLimit < 0) ==========*/
	  T_tuple_length(hv_WidthLimit,&TTemp[SP++]);
	  create_tuple_i(&TTemp[SP++],0);
	  T_tuple_equal(TTemp[SP-2],TTemp[SP-1],&TTemp[SP]);
	  destroy_tuple(TTemp[SP-2]);
	  destroy_tuple(TTemp[SP-1]);
	  TTemp[SP-2]=TTemp[SP];
	  SP=SP-1;
	  create_tuple_i(&TTemp[SP++],0);
	  T_tuple_less(hv_WidthLimit,TTemp[SP-1],&TTemp[SP]);
	  destroy_tuple(TTemp[SP-1]);
	  TTemp[SP-1]=TTemp[SP];
	  T_tuple_or(TTemp[SP-2],TTemp[SP-1],&TTemp[SP]);
	  destroy_tuple(TTemp[SP-2]);
	  destroy_tuple(TTemp[SP-1]);
	  TTemp[SP-2]=TTemp[SP];
	  SP=SP-1;
	  if(get_i(TTemp[SP-1],0))
	  {
		/*MinWidth := 500*/
		reuse_tuple_i(&hv_MinWidth,500);

		/*MaxWidth := 800*/
		reuse_tuple_i(&hv_MaxWidth,800);

	  }
	  else
	  {
	  destroy_tuple(TTemp[--SP]);
	  /*========== elseif (|WidthLimit| == 1) ==========*/

	  T_tuple_length(hv_WidthLimit,&TTemp[SP++]);
	  create_tuple_i(&TTemp[SP++],1);
	  T_tuple_equal(TTemp[SP-2],TTemp[SP-1],&TTemp[SP]);
	  destroy_tuple(TTemp[SP-2]);
	  destroy_tuple(TTemp[SP-1]);
	  TTemp[SP-2]=TTemp[SP];
	  SP=SP-1;
	  if(get_i(TTemp[SP-1],0))
	  {
		/*MinWidth := 0*/
		reuse_tuple_i(&hv_MinWidth,0);

		/*MaxWidth := WidthLimit*/
		destroy_tuple(hv_MaxWidth);
		copy_tuple(hv_WidthLimit,&hv_MaxWidth);

	  }
	  else
	  {
		/*MinWidth := WidthLimit[0]*/
		create_tuple_i(&TTemp[SP++],0);
		T_tuple_select(hv_WidthLimit,TTemp[SP-1],&TTemp[SP]);
		destroy_tuple(TTemp[SP-1]);
		TTemp[SP-1]=TTemp[SP];
		destroy_tuple(hv_MinWidth);
		hv_MinWidth=TTemp[--SP];

		/*MaxWidth := WidthLimit[1]*/
		create_tuple_i(&TTemp[SP++],1);
		T_tuple_select(hv_WidthLimit,TTemp[SP-1],&TTemp[SP]);
		destroy_tuple(TTemp[SP-1]);
		TTemp[SP-1]=TTemp[SP];
		destroy_tuple(hv_MaxWidth);
		hv_MaxWidth=TTemp[--SP];

	  }
	  }
	  destroy_tuple(TTemp[--SP]);
	  /*========== end if ==========*/
	  /*Parse input tuple HeightLimit*/
	  /*========== if (|HeightLimit| == 0 or HeightLimit < 0) ==========*/
	  T_tuple_length(hv_HeightLimit,&TTemp[SP++]);
	  create_tuple_i(&TTemp[SP++],0);
	  T_tuple_equal(TTemp[SP-2],TTemp[SP-1],&TTemp[SP]);
	  destroy_tuple(TTemp[SP-2]);
	  destroy_tuple(TTemp[SP-1]);
	  TTemp[SP-2]=TTemp[SP];
	  SP=SP-1;
	  create_tuple_i(&TTemp[SP++],0);
	  T_tuple_less(hv_HeightLimit,TTemp[SP-1],&TTemp[SP]);
	  destroy_tuple(TTemp[SP-1]);
	  TTemp[SP-1]=TTemp[SP];
	  T_tuple_or(TTemp[SP-2],TTemp[SP-1],&TTemp[SP]);
	  destroy_tuple(TTemp[SP-2]);
	  destroy_tuple(TTemp[SP-1]);
	  TTemp[SP-2]=TTemp[SP];
	  SP=SP-1;
	  if(get_i(TTemp[SP-1],0))
	  {
		/*MinHeight := 400*/
		reuse_tuple_i(&hv_MinHeight,400);

		/*MaxHeight := 600*/
		reuse_tuple_i(&hv_MaxHeight,600);

	  }
	  else
	  {
	  destroy_tuple(TTemp[--SP]);
	  /*========== elseif (|HeightLimit| == 1) ==========*/

	  T_tuple_length(hv_HeightLimit,&TTemp[SP++]);
	  create_tuple_i(&TTemp[SP++],1);
	  T_tuple_equal(TTemp[SP-2],TTemp[SP-1],&TTemp[SP]);
	  destroy_tuple(TTemp[SP-2]);
	  destroy_tuple(TTemp[SP-1]);
	  TTemp[SP-2]=TTemp[SP];
	  SP=SP-1;
	  if(get_i(TTemp[SP-1],0))
	  {
		/*MinHeight := 0*/
		reuse_tuple_i(&hv_MinHeight,0);

		/*MaxHeight := HeightLimit*/
		destroy_tuple(hv_MaxHeight);
		copy_tuple(hv_HeightLimit,&hv_MaxHeight);

	  }
	  else
	  {
		/*MinHeight := HeightLimit[0]*/
		create_tuple_i(&TTemp[SP++],0);
		T_tuple_select(hv_HeightLimit,TTemp[SP-1],&TTemp[SP]);
		destroy_tuple(TTemp[SP-1]);
		TTemp[SP-1]=TTemp[SP];
		destroy_tuple(hv_MinHeight);
		hv_MinHeight=TTemp[--SP];

		/*MaxHeight := HeightLimit[1]*/
		create_tuple_i(&TTemp[SP++],1);
		T_tuple_select(hv_HeightLimit,TTemp[SP-1],&TTemp[SP]);
		destroy_tuple(TTemp[SP-1]);
		TTemp[SP-1]=TTemp[SP];
		destroy_tuple(hv_MaxHeight);
		hv_MaxHeight=TTemp[--SP];

	  }
	  }
	  destroy_tuple(TTemp[--SP]);
	  /*========== end if ==========*/
	  /**/
	  /*Test, if window size has to be changed.*/
	  /*ResizeFactor := 1*/
	  reuse_tuple_i(&hv_ResizeFactor,1);

	  /*get_image_size (Image, ImageWidth, ImageHeight)*/
	  destroy_tuple(hv_ImageWidth);
	  destroy_tuple(hv_ImageHeight);
	  /***/T_get_image_size(ho_Image, &hv_ImageWidth, &hv_ImageHeight);

	  /*First, expand window to the minimum extents (if necessary).*/
	  /*========== if (MinWidth > ImageWidth or MinHeight > ImageHeight) ==========*/
	  T_tuple_greater(hv_MinWidth,hv_ImageWidth,&TTemp[SP++]);
	  T_tuple_greater(hv_MinHeight,hv_ImageHeight,&TTemp[SP++]);
	  T_tuple_or(TTemp[SP-2],TTemp[SP-1],&TTemp[SP]);
	  destroy_tuple(TTemp[SP-2]);
	  destroy_tuple(TTemp[SP-1]);
	  TTemp[SP-2]=TTemp[SP];
	  SP=SP-1;
	  if(get_i(TTemp[SP-1],0))
	  {
		/*ResizeFactor := max([real(MinWidth) / ImageWidth,real(MinHeight) / ImageHeight])*/
		T_tuple_real(hv_MinWidth,&TTemp[SP++]);
		T_tuple_div(TTemp[SP-1],hv_ImageWidth,&TTemp[SP]);
		destroy_tuple(TTemp[SP-1]);
		TTemp[SP-1]=TTemp[SP];
		T_tuple_real(hv_MinHeight,&TTemp[SP++]);
		T_tuple_div(TTemp[SP-1],hv_ImageHeight,&TTemp[SP]);
		destroy_tuple(TTemp[SP-1]);
		TTemp[SP-1]=TTemp[SP];
		T_tuple_concat(TTemp[SP-2],TTemp[SP-1],&TTemp[SP]);
		destroy_tuple(TTemp[SP-2]);
		destroy_tuple(TTemp[SP-1]);
		TTemp[SP-2]=TTemp[SP];
		SP=SP-1;
		T_tuple_max(TTemp[SP-1],&TTemp[SP]);
		destroy_tuple(TTemp[SP-1]);
		TTemp[SP-1]=TTemp[SP];
		destroy_tuple(hv_ResizeFactor);
		hv_ResizeFactor=TTemp[--SP];

	  }
	  destroy_tuple(TTemp[--SP]);
	  /*========== end if ==========*/
	  /*TempWidth := ImageWidth * ResizeFactor*/
	  T_tuple_mult(hv_ImageWidth,hv_ResizeFactor,&TTemp[SP++]);
	  destroy_tuple(hv_TempWidth);
	  hv_TempWidth=TTemp[--SP];

	  /*TempHeight := ImageHeight * ResizeFactor*/
	  T_tuple_mult(hv_ImageHeight,hv_ResizeFactor,&TTemp[SP++]);
	  destroy_tuple(hv_TempHeight);
	  hv_TempHeight=TTemp[--SP];

	  /*Then, shrink window to maximum extents (if necessary).*/
	  /*========== if (MaxWidth < TempWidth or MaxHeight < TempHeight) ==========*/
	  T_tuple_less(hv_MaxWidth,hv_TempWidth,&TTemp[SP++]);
	  T_tuple_less(hv_MaxHeight,hv_TempHeight,&TTemp[SP++]);
	  T_tuple_or(TTemp[SP-2],TTemp[SP-1],&TTemp[SP]);
	  destroy_tuple(TTemp[SP-2]);
	  destroy_tuple(TTemp[SP-1]);
	  TTemp[SP-2]=TTemp[SP];
	  SP=SP-1;
	  if(get_i(TTemp[SP-1],0))
	  {
		/*ResizeFactor := ResizeFactor * min([real(MaxWidth) / TempWidth,real(MaxHeight) / TempHeight])*/
		copy_tuple(hv_ResizeFactor,&TTemp[SP++]);
		T_tuple_real(hv_MaxWidth,&TTemp[SP++]);
		T_tuple_div(TTemp[SP-1],hv_TempWidth,&TTemp[SP]);
		destroy_tuple(TTemp[SP-1]);
		TTemp[SP-1]=TTemp[SP];
		T_tuple_real(hv_MaxHeight,&TTemp[SP++]);
		T_tuple_div(TTemp[SP-1],hv_TempHeight,&TTemp[SP]);
		destroy_tuple(TTemp[SP-1]);
		TTemp[SP-1]=TTemp[SP];
		T_tuple_concat(TTemp[SP-2],TTemp[SP-1],&TTemp[SP]);
		destroy_tuple(TTemp[SP-2]);
		destroy_tuple(TTemp[SP-1]);
		TTemp[SP-2]=TTemp[SP];
		SP=SP-1;
		T_tuple_min(TTemp[SP-1],&TTemp[SP]);
		destroy_tuple(TTemp[SP-1]);
		TTemp[SP-1]=TTemp[SP];
		T_tuple_mult(TTemp[SP-2],TTemp[SP-1],&TTemp[SP]);
		destroy_tuple(TTemp[SP-2]);
		destroy_tuple(TTemp[SP-1]);
		TTemp[SP-2]=TTemp[SP];
		SP=SP-1;
		destroy_tuple(hv_ResizeFactor);
		hv_ResizeFactor=TTemp[--SP];

	  }
	  destroy_tuple(TTemp[--SP]);
	  /*========== end if ==========*/
	  /*WindowWidth := ImageWidth * ResizeFactor*/
	  T_tuple_mult(hv_ImageWidth,hv_ResizeFactor,&TTemp[SP++]);
	  destroy_tuple(hv_WindowWidth);
	  hv_WindowWidth=TTemp[--SP];

	  /*WindowHeight := ImageHeight * ResizeFactor*/
	  T_tuple_mult(hv_ImageHeight,hv_ResizeFactor,&TTemp[SP++]);
	  destroy_tuple(hv_WindowHeight);
	  hv_WindowHeight=TTemp[--SP];

	  /*Resize window*/
	  /*dev_open_window (Row, Column, WindowWidth, WindowHeight, 'black', WindowHandle)*/
	  create_tuple_s(&TTemp[SP++],"black");
	  create_tuple_s(&TTemp[SP++],"background_color");
	  T_set_window_attr(TTemp[SP-1],TTemp[SP-2]);
	  destroy_tuple(TTemp[--SP]);
	  destroy_tuple(TTemp[--SP]);
	  create_tuple_i(&TTemp[SP++],0);
	  create_tuple_s(&TTemp[SP++],"");
	  create_tuple_s(&TTemp[SP++],"");
	  destroy_tuple((*hv_WindowHandle));
	  /***/T_open_window(hv_Row,hv_Column,hv_WindowWidth,hv_WindowHeight,TTemp[SP-3],TTemp[SP-2],TTemp[SP-1],&(*hv_WindowHandle));
	  destroy_tuple(TTemp[--SP]);
	  destroy_tuple(TTemp[--SP]);
	  destroy_tuple(TTemp[--SP]);
	  window_stack_push(get_i((*hv_WindowHandle),0));

	  if (window_stack_is_open())
	  {
		Htuple active_win;
		create_tuple_i(&active_win,window_stack_get_active());
		/*dev_set_part (0, 0, ImageHeight - 1, ImageWidth - 1)*/
		create_tuple_i(&TTemp[SP++],0);
		create_tuple_i(&TTemp[SP++],0);
		create_tuple_i(&TTemp[SP++],1);
		T_tuple_sub(hv_ImageHeight,TTemp[SP-1],&TTemp[SP]);
		destroy_tuple(TTemp[SP-1]);
		TTemp[SP-1]=TTemp[SP];
		create_tuple_i(&TTemp[SP++],1);
		T_tuple_sub(hv_ImageWidth,TTemp[SP-1],&TTemp[SP]);
		destroy_tuple(TTemp[SP-1]);
		TTemp[SP-1]=TTemp[SP];
		/***/T_set_part(active_win,TTemp[SP-4], TTemp[SP-3], TTemp[SP-2], TTemp[SP-1]);
		destroy_tuple(TTemp[--SP]);
		destroy_tuple(TTemp[--SP]);
		destroy_tuple(TTemp[--SP]);
		destroy_tuple(TTemp[--SP]);
		destroy_tuple(active_win);
	  }
	  /*========== return ==========*/

	  /* Clear temporary tuple stack */
	  while (SP > 0)
		destroy_tuple(TTemp[--SP]);
	  /* Clear temporary object stack */
	  while (SPO > 0)
		clear_obj(OTemp[--SPO]);
	  /* Clear temporary tuple vectors stack*/
	  while (SPTV > 0)
		V_destroy_vector(TVTemp[--SPTV]);
	  /* Clear temporary object vectors stack */
	  while (SPOV > 0)
		V_destroy_vector(OVTemp[--SPOV]);
	  /* Clear local control variables */
	  destroy_tuple(hv_MinWidth);
	  destroy_tuple(hv_MaxWidth);
	  destroy_tuple(hv_MinHeight);
	  destroy_tuple(hv_MaxHeight);
	  destroy_tuple(hv_ResizeFactor);
	  destroy_tuple(hv_ImageWidth);
	  destroy_tuple(hv_ImageHeight);
	  destroy_tuple(hv_TempWidth);
	  destroy_tuple(hv_TempHeight);
	  destroy_tuple(hv_WindowWidth);
	  destroy_tuple(hv_WindowHeight);

	  return;

	  /****************************************************/
	  /******************     End procedure   *************/
	  /****************************************************/

}