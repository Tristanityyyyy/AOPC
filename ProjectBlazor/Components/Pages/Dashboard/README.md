# Dashboard Components

Reusable components for the DashboardAdmin page.

## Components Created

### 1. StatCard.razor
Displays a single statistic with a large number and label.
- **Use case**: Support Center, Total Concerns
- **Parameters**: Title, Value, Label, Style

### 2. LineChartCard.razor
Displays a line chart with title.
- **Use case**: Total User chart
- **Parameters**: Title, ChartSeries, ChartLabels, ChartOptions, Style

### 3. RegistrationCard.razor
Displays new user registration statistics with filters.
- **Use case**: New User Registration section
- **Parameters**: Title, NewUsers, TotalActiveUsers, RegistrationPercent, SelectedDays, DropdownOpen, event callbacks

### 4. DonutChartCard.razor
Displays a donut chart with legend and filters.
- **Use case**: Top 3 Restaurant, Hotels, Store, Wellness, Offers
- **Parameters**: Title, Data, Labels, SelectedDays, DropdownOpen, event callbacks

### 5. DataTableCard.razor
Generic data table with search, pagination, sorting, and filters.
- **Use case**: Call to Actions, Module Click Count, Company Information
- **Parameters**: Title, Style, ShowClearFilter, ShowSelectDate, ShowFilterDays, ShowSelectCategory, RenderFragments for header/rows/pagination

### 6. PaginationControl.razor
Reusable pagination control with first/prev/next/last buttons.
- **Use case**: All tables
- **Parameters**: CurrentPage, TotalPages, OnPageChanged

### 7. DashboardTopBar.razor
Top bar with date selector, filter days dropdown, and export button.
- **Use case**: Dashboard top section
- **Parameters**: SelectedDays, DropdownOpen, event callbacks

## Usage Example

```razor
@* StatCard *@
<StatCard Title="Support Center" 
          Value="@_concerns.ToString()" 
          Label="Total concern that need to attends" />

@* LineChartCard *@
<LineChartCard Title="Total User" 
               ChartSeries="@_series" 
               ChartLabels="@_xAxisLabels" />

@* RegistrationCard *@
<RegistrationCard NewUsers="@_newUsers"
                  TotalActiveUsers="@_totalActiveUsers"
                  RegistrationPercent="@_registrationPercent"
                  SelectedDays="@_selectedDays2"
                  DropdownOpen="@_dropdownOpen2"
                  OnToggleDropdown="ToggleDropdown2"
                  OnDaySelected="SelectDay2"
                  OnClearFilter="ClearFilter2"
                  OnSelectDate="OpenDatePicker2" />

@* DonutChartCard *@
<DonutChartCard Title="Top 3 Restaurant"
                Data="@_restaurantData"
                Labels="@_restaurantLabels"
                SelectedDays="@_donutSelectedDays"
                DropdownOpen="@_donutDropdownOpen"
                OnToggleDropdown="ToggleDonutDropdown"
                OnDaySelected="SelectDonutDay"
                OnSelectDate='@(() => OpenDatePicker("donut"))'
                OnViewDetails="@(() => {})" />

@* DataTableCard *@
<DataTableCard TItem="CtaItem"
               Title="Call to Actions"
               ShowSelectCategory="true"
               SelectedDays="@_ctaSelectedDays"
               SelectedCategory="@_ctaSelectedCategory"
               DaysDropdownOpen="@_ctaDropdownOpen"
               CategoryDropdownOpen="@_ctaCategoryDropdownOpen"
               Categories="@_categories"
               OnToggleDropdown="ToggleCtaDropdown"
               OnToggleCategoryDropdown="ToggleCtaCategoryDropdown"
               OnDaySelected="SelectCtaDay"
               OnCategorySelected="SelectCtaCategory"
               OnClearFilter="ClearCtaFilter"
               OnSelectDate="OpenCtaDatePicker"
               PaginationText="@($"Showing {CtaFrom} to {CtaTo} of {_ctaItems.Count} entries")"
               ExportButtonColor="@ThemeConfig.Colors.Header.Start">
    <HeaderContent>
        <th class="text-left py-2 px-2 font-semibold text-gray-700 cursor-pointer" @onclick='() => SortCta("Name")'>
            Name <span class="text-gray-400">@SortIcon(_ctaSortCol, _ctaSortAsc, "Name")</span>
        </th>
        @* More headers... *@
    </HeaderContent>
    <RowContent>
        @foreach (var item in FilteredCtaItems)
        {
            <tr class="border-b border-gray-100 hover:bg-gray-50">
                <td class="py-2 px-2 text-gray-700">@item.Name</td>
                @* More cells... *@
            </tr>
        }
    </RowContent>
    <PaginationContent>
        <PaginationControl CurrentPage="@_ctaPage" 
                          TotalPages="@CtaTotalPages" 
                          OnPageChanged="@(p => _ctaPage = p)" />
    </PaginationContent>
</DataTableCard>

@* PaginationControl *@
<PaginationControl CurrentPage="@_currentPage" 
                   TotalPages="@_totalPages" 
                   OnPageChanged="@(p => _currentPage = p)" />

@* DashboardTopBar *@
<DashboardTopBar SelectedDays="@_selectedDays"
                 DropdownOpen="@_dropdownOpen"
                 OnToggleDropdown="ToggleDropdown"
                 OnDaySelected="SelectDay"
                 OnSelectDate="OpenDatePicker"
                 OnExport="ExportData" />
```

## Benefits

1. **Reusability**: Components can be used across multiple dashboard pages
2. **Consistency**: Ensures consistent UI/UX across the application
3. **Maintainability**: Changes to component logic only need to be made once
4. **Readability**: DashboardAdmin.razor becomes much cleaner and easier to understand
5. **Testability**: Components can be tested independently
6. **Flexibility**: Parameters allow customization while maintaining consistency

## Next Steps

To use these components in DashboardAdmin.razor:
1. Replace inline RenderFragments with component tags
2. Pass data and event handlers as parameters
3. Remove duplicate code for dropdowns, pagination, etc.
4. Consider creating services to fetch data from APIs instead of hardcoded lists
