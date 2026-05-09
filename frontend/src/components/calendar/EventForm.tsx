import { Button, Checkbox, Combobox, Group, Input, InputBase, NativeSelect, NumberInput, Stack, Tabs, Textarea, TextInput, useCombobox } from "@mantine/core";
import { useForm, schemaResolver } from "@mantine/form";
import { postApiGoalCreateBearing } from "@/api/endpoints/goal/goal.js";
import { PostApiGoalCreateBearingBody } from "@/api/endpoints/goal/goal.zod.js";
import { getErrorMessage } from "@/data/error";
import { useState } from "react";

interface EventFormProps {
    close: () => void;
    setAlert: (alert: string) => void;
}

export default function EventForm({ close, setAlert }: EventFormProps) {
    const formSchema = PostApiGoalCreateBearingBody.omit({
        northStarId: true
    })
    const form = useForm({
        mode: 'controlled',
        validate: schemaResolver(formSchema, { sync: true })
    })
    function constructRRULE(values: typeof form.values) {
        let parts = [];

        parts.push(`FREQ=${values.unit}`);
        parts.push(`INTERVAL=${values.amount}`);

        switch (values.unit) {
            case "WEEKLY":
                parts.push(`BYDAY=${values.weekday}`);
                break;
            case "MONTHLY":
                parts.push(`BYMONTHDAY=${values.monthday}`);
                break;
            case "YEARLY":
                parts.push(`BYMONTH=${values.yearmonth}`)
                parts.push(`BYMONTHDAY=${values.monthday}`);
                break;
        }

        return parts.join(";");
    }
    const handleSubmit = async (values: typeof form.values) => {
        const requestData = {
            ...values
        }
        console.log(constructRRULE(values));
        return;
        const response = await postApiGoalCreateBearing(requestData);

        if (response.status === 200) {
            close();
        } else {
            setAlert(response.data ?? getErrorMessage(response.status));
        }
    };

    const unitCombobox = useCombobox({
        onDropdownClose: () => unitCombobox.resetSelectedOption()
    })
    const [unit, setUnit] = useState<string | null>(null);
    const unitOptions = [
        {label: "day", value: "DAILY"},
        {label: "week", value: "WEEKLY"},
        {label: "month", value: "MONTHLY"},
        {label: "year", value: "YEARLY"}
    ];
    const weekdays = [
        {label: "Monday", value: "MO"},
        {label: "Tuesday", value: "TU"},
        {label: "Wednesday", value: "WE"}, 
        {label: "Thursday", value:"TH"},
        {label: "Friday", value: "FR"},
        {label: "Saturday", value: "SA"},
        {label: "Sunday", value: "SU"}
    ];

    return (
        <>
            <form onSubmit={form.onSubmit(handleSubmit)}>
                <Stack>
                    <TextInput
                        label="Name"
                        description="What should this event be called?"
                        placeholder="1"
                        required
                        key={form.key('name')}
                        {...form.getInputProps('name')}
                    />


                    <Group grow justify="flex-between">
                        <NumberInput
                            label="Amount"
                            description="What should this event be called?"
                            placeholder="1"
                            required
                            key={form.key('amount')}
                            {...form.getInputProps('amount')}
                        />
                        <NativeSelect
                            label="Unit"
                            description="What should this event be called?"
                            required
                            data={unitOptions}
                            key={form.key('unit')}
                            {...form.getInputProps('unit')}
                        />
                    </Group>

                    {form.values.unit == "WEEKLY" && (
                        <Checkbox.Group
                            label="Day of the week"
                            description="Which day of the week should this event take place?"
                            required
                            key={form.key('weekday')}
                            {...form.getInputProps('weekday')}
                        >
                            <Group mt="xs">
                                {weekdays.map((weekday) => (
                                    <Checkbox key={weekday.value} label={weekday.label} value={weekday.value}/>
                                ))}
                            </Group>
                        </Checkbox.Group>
                    )}

                    {form.values.unit == "MONTHLY" && (
                        <NumberInput
                            label="Day of the month"
                            description="Which day of the month should this event take place?"
                            required
                            key={form.key('monthday')}
                            {...form.getInputProps('monthday')}
                        />
                    )}

                    {form.values.unit == "YEARLY" && (
                        <Group grow justify="flex-between">
                            <NumberInput
                                label="Day of the month"
                                description="Which day of the month should this event take place?"
                                required
                                key={form.key('monthday')}
                                {...form.getInputProps('monthday')}
                            />
                            <NumberInput
                                label="Month of the year"
                                description="Which month of the eyar should this event take place?"
                                required
                                key={form.key('yearmonth')}
                                {...form.getInputProps('yearmonth')}
                            />
                        </Group>
                    )}

                    <Group justify="flex-end">
                        <Button type="submit">Submit</Button>
                    </Group>
                </Stack>
            </form>
        </>
    )
}