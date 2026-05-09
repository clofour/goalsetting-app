import { Button, Checkbox, Group, Input, NumberInput, SegmentedControl, Select, Stack, TextInput, useCombobox } from "@mantine/core";
import { useForm, schemaResolver } from "@mantine/form";
import { DatePickerInput, TimePicker } from "@mantine/dates";
import { postApiGoalCreateBearing } from "@/api/endpoints/goal/goal.js";
import { PostApiGoalCreateBearingBody } from "@/api/endpoints/goal/goal.zod.js";
import { getErrorMessage } from "@/data/error";
import { useState } from "react";
import { postApiEventCreateOnetime } from "@/api/endpoints/event/event";

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
        const response = await postApiEventCreateOnetime(requestData);

        if (response.status === 200) {
            close();
        } else {
            setAlert(response.data ?? getErrorMessage(response.status));
        }
    };

    const eventTypes = [
        { label: "One-time", value: "onetime" },
        { label: "Recurring", value: "recurring" }
    ]
    const unitCombobox = useCombobox({
        onDropdownClose: () => unitCombobox.resetSelectedOption()
    })
    const unitOptions = [
        { label: "day", value: "DAILY" },
        { label: "week", value: "WEEKLY" },
        { label: "month", value: "MONTHLY" },
        { label: "year", value: "YEARLY" }
    ];
    const weekdays = [
        { label: "Monday", value: "MO" },
        { label: "Tuesday", value: "TU" },
        { label: "Wednesday", value: "WE" },
        { label: "Thursday", value: "TH" },
        { label: "Friday", value: "FR" },
        { label: "Saturday", value: "SA" },
        { label: "Sunday", value: "SU" }
    ];

    return (
        <>
            <form onSubmit={form.onSubmit(handleSubmit)}>
                <Stack>
                    <TextInput
                        label="Name"
                        description="What should this event be called?"
                        placeholder="Go to the gym"
                        required
                        key={form.key('name')}
                        {...form.getInputProps('name')}
                    />

                    <DatePickerInput
                        label="Start date"
                        description="When should this event start?"
                        key={form.key('startDate')}
                        {...form.getInputProps('startDate')}
                    />

                    <TimePicker
                        label="Duration"
                        description="How long should this event last?"
                        type="duration"
                        key={form.key('duration')}
                        {...form.getInputProps('duration')}
                    />

                    <Input.Wrapper
                        label="Type"
                        description="Should this event be recurring?"
                    >
                        <SegmentedControl

                            data={eventTypes}
                            fullWidth
                            key={form.key('type')}
                            {...form.getInputProps('type')}
                        />
                    </Input.Wrapper>

                    {form.values.type == "recurring" && (
                        <>
                            <Group grow justify="flex-between">
                                <NumberInput
                                    label="Amount"
                                    description="How many recurrence units should there be?"
                                    placeholder="1"
                                    required
                                    key={form.key('amount')}
                                    {...form.getInputProps('amount')}
                                />
                                <Select
                                    label="Unit"
                                    description="What should the recurrence unit be?"
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
                                            <Checkbox key={weekday.value} label={weekday.label} value={weekday.value} />
                                        ))}
                                    </Group>
                                </Checkbox.Group>
                            )}

                            {form.values.unit == "MONTHLY" && (
                                <NumberInput
                                    label="Day of the month"
                                    description="Which day of the month should this event take place?"
                                    min={1}
                                    max={31}
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
                        </>
                    )}

                    <Group justify="flex-end">
                        <Button type="submit">Submit</Button>
                    </Group>
                </Stack>
            </form>
        </>
    )
}